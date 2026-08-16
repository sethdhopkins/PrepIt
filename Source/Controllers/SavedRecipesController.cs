using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Source.Controllers
{
    public class SavedRecipesController : Controller
    {
        private readonly SourceContext _context;

        public SavedRecipesController(SourceContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var savedRecipes = await _context.SavedRecipe
                .Include(sr => sr.Recipe)
                .ToListAsync();
            return View(savedRecipes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(List<int> selectedSavedRecipeIds, int userId = 1)
        {

            foreach (var recipeId in selectedSavedRecipeIds)
            {
                var recipe = await _context.SavedRecipe.FindAsync(recipeId);
                if (recipe != null)
                {
                    _context.SavedRecipe.Remove(recipe);
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
