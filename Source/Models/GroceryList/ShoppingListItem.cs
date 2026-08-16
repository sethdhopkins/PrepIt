using Source.Models.Ingredients;

namespace Source.Models.GroceryList
{
    public class ShoppingListItem
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public required string Unit { get; set; }
        public bool Purchased { get; set; } = false;
        public int IngredientId { get; set; }
        public int ShoppingListId { get; set; } 
        public Ingredient Ingredient { get; set; } = null!;
        public ShoppingList ShoppingList { get; set; } = null!;

    }
}
