using Source.Models.MealPlanning;

namespace Source.Services
{
    public class MealPlanService : IMealPlanService
    {
        public async Task CreateMeal(int recipeId, DateTime mealDate, Meal.MealType mealType)
        {
            throw new NotImplementedException();
        }

        public async Task AddMealToPlanAsync(Meal meal)
        {
            throw new NotImplementedException();
        }

        public Task CreateMealAsnyc(int recipId, DateTime mealDate, Meal.MealType mealType, int userId)
        {
            throw new NotImplementedException();
        }

        public Task CreateMealPlanAsync(Meal meal)
        {
            throw new NotImplementedException();
        }

        public Task GetMealPlan(Meal meal)
        {
            throw new NotImplementedException();
        }

        public Task RemoveMealFromPlanAsync(Meal meal)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMealAsync(Meal meal)
        {
            throw new NotImplementedException();
        }

        public Task RemoveMealFromQueueAsync(Meal meal)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMealFromQueueAsync(Meal meal)
        {
            throw new NotImplementedException();
        }

        Task<MealPlan> IMealPlanService.GetMealPlan(Meal meal)
        {
            throw new NotImplementedException();
        }
    }
}
