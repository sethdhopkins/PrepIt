using Microsoft.EntityFrameworkCore;
using Source.Models.MealPlanning;
using Source.Models.Recipes;

namespace Source.Services
{
    public class MealPlanService : IMealPlanService
    {
        private readonly SourceContext _context;

        public MealPlanService(SourceContext context)
        {
            _context = context;
        }
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

        public async Task<IEnumerable<RecipeDto>> GetQueueItemsAsync(int userId)
        {
            return await _context.RecipeQueue
                .Where(q => q.UserId == userId)
                .SelectMany(q => q.RecipeQueueItems)
                .OrderBy(qi => qi.TimeAdded)
                .Select(qi => new RecipeDto
                {
                    Id = qi.Recipe.Id,
                    Name = qi.Recipe.Name,
                    Image = qi.Recipe.Image
                })
                .ToListAsync();
        }
    }
}
