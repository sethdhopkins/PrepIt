using Microsoft.EntityFrameworkCore;
using Source.Api.MealDb;
using Source.Models.Ingredients;
using Source.Models.MealPlanning;
using Source.Models.Recipes;
using Source.Models.Users;
using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Source.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly HttpClient _httpClient;
        private readonly SourceContext _context;

        public RecipeService(HttpClient httpClient, SourceContext context)
        {   
            _httpClient = httpClient;
            _context = context;
        }

        private decimal? ParseQuantity(string measure)
        {
            if (string.IsNullOrWhiteSpace(measure))
                return null;

            measure = measure.Trim();

            // Mixed fraction: "1 1/2 cups"
            var mixedFraction = Regex.Match(
                measure,
                @"^\s*(\d+)\s+(\d+)\s*/\s*(\d+)"
            );

            if (mixedFraction.Success)
            {
                var whole = decimal.Parse(
                    mixedFraction.Groups[1].Value,
                    CultureInfo.InvariantCulture);

                var numerator = decimal.Parse(
                    mixedFraction.Groups[2].Value,
                    CultureInfo.InvariantCulture);

                var denominator = decimal.Parse(
                    mixedFraction.Groups[3].Value,
                    CultureInfo.InvariantCulture);

                return whole + numerator / denominator;
            }

            // Fraction: "1/2 cup"
            var fraction = Regex.Match(
                measure,
                @"^\s*(\d+)\s*/\s*(\d+)"
            );

            if (fraction.Success)
            {
                var numerator = decimal.Parse(
                    fraction.Groups[1].Value,
                    CultureInfo.InvariantCulture);

                var denominator = decimal.Parse(
                    fraction.Groups[2].Value,
                    CultureInfo.InvariantCulture);

                return numerator / denominator;
            }

            // Decimal/integer: "3 Medium", "1.5 cups", "2 strips"
            var number = Regex.Match(
                measure,
                @"^\s*(\d+(?:\.\d+)?)"
            );

            if (number.Success &&
                decimal.TryParse(
                    number.Groups[1].Value,
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out var quantity))
            {
                return quantity;
            }

            return null;
        }

        private string? ParseUnit(string measure)
        {
            if (string.IsNullOrWhiteSpace(measure))
                return null;

            measure = measure.Trim();

            // Remove mixed fraction: "1 1/2 cups" -> "cups"
            var mixedFraction = Regex.Match(
                measure,
                @"^\s*\d+\s+\d+\s*/\s*\d+\s*(.*)$"
            );

            if (mixedFraction.Success)
                return NormalizeUnit(mixedFraction.Groups[1].Value);

            // Remove fraction: "1/2 cup" -> "cup"
            var fraction = Regex.Match(
                measure,
                @"^\s*\d+\s*/\s*\d+\s*(.*)$"
            );

            if (fraction.Success)
                return NormalizeUnit(fraction.Groups[1].Value);

            // Remove number: "3 Medium" -> "Medium"
            var number = Regex.Match(
                measure,
                @"^\s*\d+(?:\.\d+)?\s*(.*)$"
            );

            if (number.Success)
                return NormalizeUnit(number.Groups[1].Value);

            // No number: "Minced", "Garnish", "Pinch", "To taste"
            return NormalizeUnit(measure);
        }

        private static string? NormalizeUnit(string value)
        {
            value = value.Trim();

            if (string.IsNullOrWhiteSpace(value))
                return null;

            return value.ToLowerInvariant() switch
            {
                "tbs" => "tbsp",
                "tbsp" => "tbsp",
                "tablespoon" => "tbsp",
                "tablespoons" => "tbsp",

                "tsp" => "tsp",
                "teaspoon" => "tsp",
                "teaspoons" => "tsp",

                "oz" => "oz",
                "ounce" => "oz",
                "ounces" => "oz",

                "lb" => "lb",
                "lbs" => "lb",
                "pound" => "lb",
                "pounds" => "lb",

                "g" => "g",
                "gram" => "g",
                "grams" => "g",

                "kg" => "kg",
                "kilogram" => "kg",
                "kilograms" => "kg",

                "ml" => "ml",
                "milliliter" => "ml",
                "milliliters" => "ml",

                "l" => "l",
                "liter" => "l",
                "liters" => "l",

                "cup" => "cup",
                "cups" => "cup",

                "medium" => "medium",
                "large" => "large",
                "small" => "small",

                "strip" => "strips",
                "strips" => "strips",

                _ => value
            };
        }

        public async Task<List<MealDbRecipe>> GetRecipesAsync(string searchTerm)
        {
            var response = await _httpClient.GetAsync($"search.php?s={searchTerm}");

            var httpResponseMessage = response.EnsureSuccessStatusCode();
            using HttpResponseMessage _ = httpResponseMessage;

            var content = await response.Content.ReadAsStringAsync();

            var recipes = JsonSerializer.Deserialize<MealDbResponse>(
                content,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return recipes?.Meals ?? new List<MealDbRecipe>();
        }

        public async Task<Recipe?> CreateAPIRecipe(string recipeId)
        {
            var dbRecipe = await _context.Recipe
                .Include(r => r.RecipeIngredients)
                .FirstOrDefaultAsync(r => r.ApiId == recipeId);

            if (dbRecipe != null)
            {
                return dbRecipe;
            }

            var response = await _httpClient.GetAsync($"lookup.php?i={recipeId}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonSerializer.Deserialize<MealDbResponse>(
                content,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            var meal = apiResponse?.Meals?.FirstOrDefault();

            if (meal == null)
            {
                return null;
            }

            dbRecipe = new Recipe
            {
                ApiId = meal.IdMeal!,
                Name = meal.StrMeal!,
                Category = meal.StrCategory,
                Area = meal.StrArea,
                Instructions = meal.StrInstructions,
                Image = meal.StrMealThumb,
                YoutubeUrl = meal.StrYoutube
            };

            // Remove duplicate ingredients from the API response
            var ingredients = meal.GetIngredients()
                .Where(i => !string.IsNullOrWhiteSpace(i.Name))
                .GroupBy(
                    i => i.Name.Trim(),
                    StringComparer.OrdinalIgnoreCase)
                .Select(g => g.First());

            foreach (var apiIngredient in ingredients)
            {
                var ingredientName = apiIngredient.Name.Trim();

                var ingredient = await _context.Ingredient
                    .FirstOrDefaultAsync(i => i.Name == ingredientName);

                if (ingredient == null)
                {
                    ingredient = new Ingredient
                    {
                        Name = ingredientName
                    };

                    _context.Ingredient.Add(ingredient);
                }

                dbRecipe.RecipeIngredients.Add(new RecipeIngredient
                {
                    Recipe = dbRecipe,
                    Ingredient = ingredient,
                    Quantity = ParseQuantity(apiIngredient.Measure),
                    Unit = ParseUnit(apiIngredient.Measure)
                });
            }

            _context.Recipe.Add(dbRecipe);

            return dbRecipe;
        }

        public async Task ToQueueAsync(string recipeId, int userId)
        {
            var dbRecipe = await CreateAPIRecipe(recipeId);

            if(dbRecipe == null)
            {
                return;
            }

            var queueItem = new RecipeQueue
            {
                Recipe = dbRecipe,
                UserId = userId
            };

            _context.RecipeQueue.Add(queueItem);

            await _context.SaveChangesAsync();
        }

        public async Task ToSavedAsync(string recipeId, int userId)
        {
            var dbRecipe = await CreateAPIRecipe(recipeId);

            if (dbRecipe == null)
            {
                return;
            }

            var recipeSaved = new SavedRecipe
            {
                Recipe = dbRecipe,
                UserId = userId
            };

            _context.SavedRecipe.Add(recipeSaved);

            await _context.SaveChangesAsync();
        }
    }

}
