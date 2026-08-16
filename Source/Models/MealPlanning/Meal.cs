using Source.Models.Recipes;
using System.ComponentModel.DataAnnotations;

namespace Source.Models.MealPlanning
{
    public class Meal
    {
        public enum MealType
        {
            Breakfast,
            Lunch,
            Dinner,
            Snack
        }
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public MealType Type { get; set; }
        public bool Cooked { get; set; } = false;
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null!;
        public int MealPlanId { get; set; }
        public MealPlan MealPlan { get; set; } = null!;
    }
}