using Source.Models.MealPlanning;
using Source.Models.Recipes;

namespace Source.Services
{
    public interface IMealPlanService
    {
        Task CreateMealAsnyc(int recipId, DateTime mealDate, Meal.MealType mealType, int userId);
        Task DeleteMealAsync(Meal meal);
        Task<IEnumerable<RecipeDto>> GetQueueItemsAsync(int userId);
        Task RemoveMealFromQueueAsync(Meal meal);
        Task DeleteMealFromQueueAsync(Meal meal);
        Task CreateMealPlanAsync(Meal meal);
        Task AddMealToPlanAsync(Meal meal);
        Task<MealPlan> GetMealPlan(Meal meal);
    }
}
