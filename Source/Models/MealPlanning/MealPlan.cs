using Source.Models.Users;

namespace Source.Models.MealPlanning
{
    public class MealPlan
    {
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public ICollection<Meal> Meals { get; set; } = [];
    }
}