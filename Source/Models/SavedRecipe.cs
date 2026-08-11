namespace Source.Models
{
    public class SavedRecipe
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Image { get; set; }
        public required string Instructions { get; set; }
        public required string Ingredients { get; set; }
        public int APIId { get; set; }
        public required int UserId { get; set; }
    }
}
