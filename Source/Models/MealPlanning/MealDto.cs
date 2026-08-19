namespace Source.Models.MealPlanning
{
    public class MealDto
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public DateOnly Date { get; set; }
        public Meal.MealType Type { get; set; }
        public bool Cooked { get; set; }
    }
}
