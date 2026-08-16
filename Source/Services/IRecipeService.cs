using Source.Api.MealDb;

namespace Source.Services

{
    public interface IRecipeService
    {
        Task<List<MealDbRecipe>> GetRecipesAsync(string searchTerm);
        Task ToQueueAsync(string recipeId, int userId);
        Task ToSavedAsync(string recipeId, int userId);
    }
}
