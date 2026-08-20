using Source.Models.Recipes;

namespace Source.Models.MealPlanning
{
    public class MealDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Image { get; set; }
        public int RecipeId { get; set; }
        public DateOnly MealDate { get; set; }
        public Meal.MealType Type { get; set; }
        public bool Cooked { get; set; } = false;
        public MealPlanDto PlanDto { get; set; } = null!;
    }
}
