using Microsoft.AspNetCore.Mvc;
using Source.Services;

namespace Source.Controllers
{
    public class FindRecipesController : Controller
    {
        private readonly IRecipeService _recipeService;

        public FindRecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(string? searchTerm) 
        {
            var recipe = await _recipeService.GetRecipesAsync(searchTerm ?? "");
            return View(recipe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToQueue(List<string> selectedRecipeIds, int userId = 1)
        {
            if (selectedRecipeIds == null || !selectedRecipeIds.Any())
            {
                TempData["Message"] = "Please select at least one recipe.";
                return RedirectToAction("Index");
            }

            foreach (var recipeId in selectedRecipeIds)
            {
                await _recipeService.ToQueueAsync(recipeId, userId);
                
            }

            TempData["Message"] = $"{selectedRecipeIds.Count} recipe(s) added to your queue.";

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToSaved(List<string> selectedRecipeIds, int userId = 1)
        {
            if (selectedRecipeIds == null || !selectedRecipeIds.Any())
            {
                TempData["Message"] = "Please select at least one recipe.";
                return RedirectToAction("Index");
            }

            foreach (var recipeId in selectedRecipeIds)
            {
               await _recipeService.ToSavedAsync(recipeId, userId);

            }

            TempData["Message"] = $"{selectedRecipeIds.Count} recipe(s) added to your favorites.";

            return RedirectToAction("Index");
        }


    }
}
