namespace Source.Models.MealPlanning
{
    public class MealPlanDto
    {
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public ICollection<MealDto> Meals { get; set; } = [];
    }
}
