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

        private async Task<MealPlan> CreateMealPlanAsync(int userId, DateOnly startDate)
        {
            var mealPlan = new MealPlan
            {
                StartDate = startDate,
                EndDate = startDate.AddDays(6),
                UserId = userId,
            };

            _context.MealPlan.Add(mealPlan);

            await _context.SaveChangesAsync();

            return mealPlan;
        }

        public async Task CreateMealAsync(CreateMealDto dto, int userId)
        {
            var mealPlan = await _context.MealPlan
                .FirstOrDefaultAsync(p =>
                p.UserId == userId &&
                p.StartDate <= (dto.MealDate) &&
                p.EndDate >= (dto.MealDate));

            if (mealPlan == null)
            {
                var startDate = dto.MealDate.AddDays(-(int)dto.MealDate.DayOfWeek);
                mealPlan = await CreateMealPlanAsync(userId, startDate);
            }

            await DeleteMealFromQueueAsync(dto.Id, userId);

            var meal = new Meal
            {
                RecipeId = dto.RecipeId,
                Date = dto.MealDate,
                Type = dto.Type,
                MealPlanId = mealPlan.Id,
            };

            _context.Meal.Add(meal);
            await _context.SaveChangesAsync();

        }

        public Task RemoveMealFromPlanAsync(Meal meal)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteMealAsync(int mealId)
        {
            var meal = await _context.Meal.FindAsync(mealId);
            if (meal != null)
            {
                _context.Meal.Remove(meal);
            }

            await _context.SaveChangesAsync();

        }

        public async Task DeleteMealFromQueueAsync(int queueItemId, int userId)
        {
            var queueItem = await _context.RecipeQueueItems
                .FirstOrDefaultAsync(qi =>
                qi.Id == queueItemId &&
                qi.RecipeQueue.UserId == userId);


            if (queueItem != null)
            {
                _context.RecipeQueueItems.Remove(queueItem);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<MealPlanDto?> GetMealPlanAsync(int userId, DateOnly startDate)
        {
            return await _context.MealPlan
                .Where(p =>
                    p.UserId == userId &&
                    p.StartDate == startDate)
                        .Select(p => new MealPlanDto
                        {
                            Id = p.Id,
                            StartDate = p.StartDate,
                            EndDate = p.EndDate,
                            Meals = p.Meals
                                .Select(m => new MealDto
                                {
                                    Id = m.Id,
                                    Name = m.Recipe.Name,
                                    Image = m.Recipe.Image,
                                    RecipeId = m.RecipeId,
                                    MealDate = m.Date,
                                    Type = m.Type,
                                    Cooked = m.Cooked
                                })
                                .ToList()
                        })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<QueueItemDto>> GetQueueItemsAsync(int userId)
        {
            return await _context.RecipeQueue
                .Where(q => q.UserId == userId)
                .SelectMany(q => q.RecipeQueueItems)
                .OrderBy(qi => qi.TimeAdded)
                .Select(qi => new QueueItemDto
                {
                    Id = qi.Id,
                    RecipeId = qi.RecipeId,
                    Name = qi.Recipe.Name,
                    Image = qi.Recipe.Image
                })
                .ToListAsync();
        }
    }
}
