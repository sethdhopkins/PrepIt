using Source.Models.Users;
namespace Source.Models.Recipes
{
    public class SavedRecipe
    {
        public int Id { get; set; }
        public int RecipeId { get; set; } = 0;
        public Recipe Recipe { get; set;  } = null!;
        public required int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
