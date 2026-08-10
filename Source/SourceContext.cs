using Microsoft.EntityFrameworkCore;

public class SourceContext(DbContextOptions<SourceContext> options) : DbContext(options)
{
    public DbSet<Source.Models.Meal> Meal { get; set; } = default!;
}
