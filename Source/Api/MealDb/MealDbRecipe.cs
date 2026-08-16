namespace Source.Api.MealDb
{
    public class MealDbRecipe
    {
        public string? IdMeal { get; set; }
        public string? StrMeal { get; set; }
        public string? StrCategory { get; set; }
        public string? StrArea { get; set; }
        public string? StrInstructions { get; set; }
        public string? StrMealThumb { get; set; }
        public string? StrYoutube { get; set; }

        public string? StrIngredient1 { get; set; }
        public string? StrIngredient2 { get; set; }
        public string? StrIngredient3 { get; set; }
        public string? StrIngredient4 { get; set; }
        public string? StrIngredient5 { get; set; }
        public string? StrIngredient6 { get; set; }
        public string? StrIngredient7 { get; set; }
        public string? StrIngredient8 { get; set; }
        public string? StrIngredient9 { get; set; }
        public string? StrIngredient10 { get; set; }
        public string? StrIngredient11 { get; set; }
        public string? StrIngredient12 { get; set; }
        public string? StrIngredient13 { get; set; }
        public string? StrIngredient14 { get; set; }
        public string? StrIngredient15 { get; set; }
        public string? StrIngredient16 { get; set; }
        public string? StrIngredient17 { get; set; }
        public string? StrIngredient18 { get; set; }
        public string? StrIngredient19 { get; set; }
        public string? StrIngredient20 { get; set; }

        public string? StrMeasure1 { get; set; }
        public string? StrMeasure2 { get; set; }
        public string? StrMeasure3 { get; set; }
        public string? StrMeasure4 { get; set; }
        public string? StrMeasure5 { get; set; }
        public string? StrMeasure6 { get; set; }
        public string? StrMeasure7 { get; set; }
        public string? StrMeasure8 { get; set; }
        public string? StrMeasure9 { get; set; }
        public string? StrMeasure10 { get; set; }
        public string? StrMeasure11 { get; set; }
        public string? StrMeasure12 { get; set; }
        public string? StrMeasure13 { get; set; }
        public string? StrMeasure14 { get; set; }
        public string? StrMeasure15 { get; set; }
        public string? StrMeasure16 { get; set; }
        public string? StrMeasure17 { get; set; }
        public string? StrMeasure18 { get; set; }
        public string? StrMeasure19 { get; set; }
        public string? StrMeasure20 { get; set; }

        public IEnumerable<(string Name, string? Measure)> GetIngredients()
        {
            var ingredients = new[]
            {
                (StrIngredient1, StrMeasure1),
                (StrIngredient2, StrMeasure2),
                (StrIngredient3, StrMeasure3),
                (StrIngredient4, StrMeasure4),
                (StrIngredient5, StrMeasure5),
                (StrIngredient6, StrMeasure6),
                (StrIngredient7, StrMeasure7),
                (StrIngredient8, StrMeasure8),
                (StrIngredient9, StrMeasure9),
                (StrIngredient10, StrMeasure10),
                (StrIngredient11, StrMeasure11),
                (StrIngredient12, StrMeasure12),
                (StrIngredient13, StrMeasure13),
                (StrIngredient14, StrMeasure14),
                (StrIngredient15, StrMeasure15),
                (StrIngredient16, StrMeasure16),
                (StrIngredient17, StrMeasure17),
                (StrIngredient18, StrMeasure18),
                (StrIngredient19, StrMeasure19),
                (StrIngredient20, StrMeasure20)
             };

            return ingredients
                .Where(x => !string.IsNullOrWhiteSpace(x.Item1))
                .Select(x => (x.Item1!, x.Item2));
        }
    }

}
