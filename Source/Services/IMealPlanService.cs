using Source.Models.MealPlanning;
using Source.Models.Recipes;

namespace Source.Services
{
    public interface IMealPlanService
    {
        Task CreateMealAsync(CreateMealDto dto, int userId);
        Task DeleteMealAsync(int mealId);
        Task<IEnumerable<QueueItemDto>> GetQueueItemsAsync(int userId);
        Task DeleteMealFromQueueAsync(int queueItemId, int userId);
        Task<MealPlanDto?> GetMealPlanAsync(int userId, DateOnly startDate);
    }
}
