namespace Source.Models.Recipes
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Image { get; set; } = string.Empty;
    }
}
