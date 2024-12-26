namespace AppleShop.Share.Events.Inventory.Request
{
    public class CheckStockEvent
    {
        public int? ProductId { get; set; }
        public int? Stock {  get; set; }
    }
}