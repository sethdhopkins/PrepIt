using Source.Models.Users;

namespace Source.Models.MealPlanning
{
    public class RecipeQueue
    {
        public int Id { get; set; }
        public ICollection<RecipeQueueItem> RecipeQueueItems { get; set; } = [];
        public required int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}