using Source.Models.Recipes;

namespace Source.Models.MealPlanning
{
    public class RecipeQueue
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null!;
        public DateTime TimeAdded { get; set; } = DateTime.Now;
        public required int UserId { get; set; }
    }
}