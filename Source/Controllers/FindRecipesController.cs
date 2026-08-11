using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Source.Migrations;
using Source.Models;
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


    }
}
