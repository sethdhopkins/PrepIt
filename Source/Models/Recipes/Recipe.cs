using Source.Models.MealPlanning;

namespace Source.Models.Recipes
{
    public class Recipe
    {
        public int Id { get; set; }

        public string? ApiId { get; set; } = null!;

        public string Name { get; set; } = null!;
        public string? Category { get; set; }
        public string? Area { get; set; }
        public string? Instructions { get; set; }
        public string? Image { get; set; }
        public string? YoutubeUrl { get; set; }

        public ICollection<Meal> Meals { get; set; } = [];

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = [];
    }
}
