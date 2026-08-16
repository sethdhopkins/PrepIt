using Source.Models.Ingredients;
using System.Text.Json;

namespace Source.Models.Inventories
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public required string Unit { get; set; }
        public DateTime TimeAdded { get; set; } = DateTime.UtcNow;
        public int InventoryId { get; set; }
        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; } = null!;
        public Inventory Inventory { get; set; } = null!;
    }
}
