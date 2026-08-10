using System.Text.Json;

namespace Source.Models
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string? Unit { get; set; }
        public string? DateAdded { get; set; }
        public required string Name { get; set; }
        public required int InventoryId { get; set; }
        public int ApiId { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, this.GetType());
        }
    }
}
