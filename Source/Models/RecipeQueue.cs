namespace Source.Models
{
    public class RecipeQueue
    {
        public int Id { get; set; }
        public int APIId { get; set; }
        public DateTime TimeAdded { get; set; } = DateTime.Now;
        public required int UserID { get; set; }
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }
    }
}