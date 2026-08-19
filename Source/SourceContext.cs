using Microsoft.EntityFrameworkCore;
using Source.Models.Ingredients;
using Source.Models.Inventories;
using Source.Models.MealPlanning;
using Source.Models.Recipes;
using Source.Models.Users;
using Source.Models.GroceryList;

public class SourceContext(DbContextOptions<SourceContext> options) : DbContext(options)
{
    public DbSet<Meal> Meal { get; set; } = default!;
    public DbSet<MealPlan> MealPlan { get; set; } = default!;
    public DbSet<RecipeQueue> RecipeQueue { get; set; } = default!;
    public DbSet<RecipeQueueItem> RecipeQueueItems { get; set; } = default!;

    public DbSet<Inventory> Inventory { get; set; } = default!;
    public DbSet<InventoryItem> InventoryItem { get; set; } = default!;
    public DbSet<SavedRecipe> SavedRecipe { get; set; } = default!;
    public DbSet<User> User { get; set; } = default!;
    public DbSet<Recipe> Recipe { get; set; } = default!;
    public DbSet<Ingredient> Ingredient { get; set; } = default!;
    public DbSet<RecipeIngredient> RecipeIngredients { get; set; } = default!;
    public DbSet<ShoppingList> ShoppingList { get; set; } = default!;
    public DbSet<ShoppingListItem> ShoppingListItem { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<InventoryItem>()
            .Property(x => x.Quantity)
            .HasPrecision(18, 2);
        modelBuilder.Entity<ShoppingListItem>()
            .Property(x => x.Quantity)
            .HasPrecision(18, 2);
        modelBuilder.Entity<RecipeIngredient>()
            .Property(x => x.Quantity)
            .HasPrecision(18, 2);
        modelBuilder.Entity<RecipeIngredient>()
            .HasIndex(ri => new {ri.RecipeId, ri.IngredientId})
            .IsUnique();
        modelBuilder.Entity<Ingredient>()
            .HasIndex(i => i.Name)
            .IsUnique();
    }
}
