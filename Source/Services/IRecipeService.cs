using Source.Models;

namespace Source.Services

{
    public interface IRecipeService
    {
        Task<List<MealDbRecipe>> GetRecipesAsync(string searchTerm);
    }
}
