using Source.Models.Users;

namespace Source.Models.Inventories
{
    public class Inventory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public ICollection<InventoryItem> Items { get; set; } = [];

    }
}
