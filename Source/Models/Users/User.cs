using Source.Models.Inventories;
using Source.Models.MealPlanning;
using Source.Models.Recipes;
using System.ComponentModel.DataAnnotations;

namespace Source.Models.Users
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public required string PasswordHash { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public ICollection<MealPlan> MealPlans { get; set; } = [];
        public ICollection<Inventory> Inventories { get; set; } = [];
        public ICollection<RecipeQueue> RecipeQueue { get; set; } = [];
        public ICollection<SavedRecipe> SavedRecipes { get; set; } = [];
    }
}
