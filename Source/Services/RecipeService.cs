using Source.Models;
using System.Text.Json;

namespace Source.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly HttpClient _httpClient;

    public RecipeService(HttpClient httpClient)
        {   
            _httpClient = httpClient;
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

        public void GetIngredients(string result)
        {
            using JsonDocument document = JsonDocument.Parse(result);

            var meal = document.RootElement
                .GetProperty("meals")[0];

            for (int i = 1; i <= 20; i++)
            {
                var ingredient = meal
                    .GetProperty($"strIngredient{i}")
                    .GetString();

                var measure = meal
                    .GetProperty($"strMeasure{i}")
                    .GetString();

                if (!string.IsNullOrEmpty(ingredient) &&
                    !string.IsNullOrEmpty(measure))
                {
                    Console.WriteLine($"{measure} of {ingredient}");
                }
            }
        }
    }

}
