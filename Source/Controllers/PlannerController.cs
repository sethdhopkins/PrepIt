using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Source.Models.MealPlanning;
using Source.Models.Recipes;
using Source.Models.Users;
using Source.Services;

namespace Source.Controllers
{
    public class PlannerController : Controller
        {
        private readonly SourceContext _context;
        private readonly IMealPlanService _mealPlanService;

        public PlannerController(SourceContext context, IMealPlanService mealPlanService)
        {
            _context = context;
            _mealPlanService = mealPlanService;
        }

        private DateOnly GetWeekStart(DateOnly date)
        {
            return date.AddDays(-(int)date.DayOfWeek);
        }

        // GET: MEALS
        public async Task<IActionResult> Index(DateOnly? weekStart, int userId = 1)
        {

            var selectedWeek = weekStart ?? GetWeekStart(DateOnly.FromDateTime(DateTime.Today));

            var queue = await _mealPlanService.GetQueueItemsAsync(userId);

            var queueItems = queue.ToList();

            var mealPlan = await _mealPlanService.GetMealPlanAsync(userId, selectedWeek);

            var model = new RecipeQueueViewModel
            {
                Plan = mealPlan,
                Queue = queueItems,
                QueueMessage = queueItems.Count == 0
                    ? "No queued recipes."
                    : null,
                WeekStart = selectedWeek,
                WeekEnd = selectedWeek.AddDays(6),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMeal(CreateMealDto dto, int userId = 1)
        {
            await _mealPlanService.CreateMealAsync(dto, userId);

            return RedirectToAction(nameof(Index));
        }

        // GET: MEALS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // GET: MEALS/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMeal(int mealId)
        {
            await _mealPlanService.DeleteMealAsync(mealId);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteQueuedMeal(int itemId, int userId = 1)
        {
            await _mealPlanService.DeleteMealFromQueueAsync(itemId, userId);

            return RedirectToAction(nameof(Index));
        }

        private bool MealExists(int? id)
        {
            return _context.Meal.Any(e => e.Id == id);
        }
    }
}
