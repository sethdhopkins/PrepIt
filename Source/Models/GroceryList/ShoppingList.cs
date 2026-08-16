using Source.Models.Users;

namespace Source.Models.GroceryList
{
    public class ShoppingList
    {
        public int Id { get; set; }
        public bool Completed { get; set; } = false;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public ICollection<ShoppingListItem> Items { get; set; } = [];
    }
}
