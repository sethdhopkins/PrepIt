using Source.Models.Inventories;
using Source.Models.GroceryList;

namespace Source.Models.Ingredients
{
    public class Ingredient
    {
        public int Id { get; set; }
        public  required string Name { get; set; }
        public string? Unit { get; set; }

        public ICollection<InventoryItem> InventoryItems { get; set; } = [];
        public ICollection<ShoppingListItem> ShoppingListItems { get; set; } = [];
    }
}
