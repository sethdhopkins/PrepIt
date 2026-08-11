using Microsoft.EntityFrameworkCore;

public class SourceContext(DbContextOptions<SourceContext> options) : DbContext(options)
{
    public DbSet<Source.Models.Meal> Meal { get; set; } = default!;
    public DbSet<Source.Models.RecipeQueue> RecipeQueue { get; set; } = default!;

    public DbSet<Source.Models.Inventory> Inventory { get; set; } = default!;
    public DbSet<Source.Models.InventoryItem> InventoryItem { get; set; } = default!;
    public DbSet<Source.Models.SavedRecipe> SavedRecipe { get; set; } = default!;
    public DbSet<Source.Models.User> User { get; set; } = default!;
    public DbSet<Source.Models.Recipe> Recipe { get; set; } = default!;
}
