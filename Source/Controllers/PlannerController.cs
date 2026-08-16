using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Source.Models.MealPlanning;
using Source.Models.Users;

namespace Source.Controllers
{
    public class PlannerController : Controller
        {
        private readonly SourceContext _context;

        public PlannerController(SourceContext context)
        {
            _context = context;
        }

        // GET: MEALS
        public async Task<IActionResult> Index(int page = 1, int userId = 1)
        {
            page = Math.Max(1, page);

            const int pageSize = 6;

            var query = _context.RecipeQueue
                .Include(x => x.Recipe)
                .Where(x => x.UserId == userId);

            var totalCount = await query.CountAsync();

            var recipes = await query
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var model = new RecipeQueueViewModel
            {
                Recipes = recipes,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMeal(int recipeId, DateTime mealDate, Meal.MealType mealType)
        {
            var recipe = await _context.Recipe
                .FirstOrDefaultAsync(r => r.Id == recipeId);

            if (recipe == null)
            {
                return NotFound();
            }

            var meal = new Meal
            {
                RecipeId = recipeId,
                Date = mealDate,
                Type = mealType,
                Cooked = false
            };

            _context.Meal.Add(meal);

            await _context.SaveChangesAsync();

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
        public async Task<IActionResult> Delete(int? id)
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

        // POST: MEALS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var meal = await _context.Meal.FindAsync(id);
            if (meal != null)
            {
                _context.Meal.Remove(meal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MealExists(int? id)
        {
            return _context.Meal.Any(e => e.Id == id);
        }
    }
}
