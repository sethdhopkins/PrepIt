using Source.Models.Recipes;

namespace Source.Models.MealPlanning
{
    public class RecipeQueueItem
    {
        public int Id { get; set; }
        public RecipeQueue RecipeQueue { get; set; } = null!;

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null!;

        public DateTime TimeAdded { get; set; } = DateTime.Now;
    }
}
