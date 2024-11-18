namespace AppleShop.Share.Events.Inventory.Query
{
    public class GetAllStockEvent
    {
        public List<int?> ProductIds { get; set; } = new List<int?>();
    }
}