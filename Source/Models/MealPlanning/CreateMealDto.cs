namespace Source.Models.MealPlanning
{
    public class CreateMealDto
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public DateOnly MealDate { get; set; }
        public Meal.MealType Type { get; set; }
    }
}
