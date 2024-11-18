namespace AppleShop.Share.Events.Inventory.Query
{
    public class InventoryResponse
    {
        public ICollection<InventoryInfo>? Inventories { get; set; } = new List<InventoryInfo>();
    }
}