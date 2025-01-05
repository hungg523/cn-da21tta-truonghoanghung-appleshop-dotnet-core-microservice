namespace AppleShop.Share.Events.Inventory.Response
{
    public class InventoriesResponse
    {
        public ICollection<InventoryResponse>? Inventories { get; set; } = new List<InventoryResponse>();
    }
}