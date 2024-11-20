namespace AppleShop.Share.Events.Inventory.Query
{
    public class InventoriesResponse
    {
        public ICollection<InventoryResponse>? Inventories { get; set; } = new List<InventoryResponse>();
    }
}