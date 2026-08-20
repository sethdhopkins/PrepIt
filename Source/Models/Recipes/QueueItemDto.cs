namespace Source.Models.Recipes
{
    public class QueueItemDto
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Image { get; set; } = string.Empty;
    }
}
