using Source.Models.MealPlanning;

namespace Source.Services
{
    public interface IMealPlanService
    {
        Task AddMealToPlanAsync(Meal meal);
    }
}
