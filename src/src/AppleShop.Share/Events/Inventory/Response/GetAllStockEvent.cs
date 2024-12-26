namespace AppleShop.Share.Events.Inventory.Response
{
    public class GetAllStockEvent
    {
        public List<int?> ProductIds { get; set; } = new List<int?>();
    }
}