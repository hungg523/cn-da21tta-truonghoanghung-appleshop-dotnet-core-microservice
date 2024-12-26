using AppleShop.Share.Events.Common;

namespace AppleShop.Share.Events.Inventory.Request
{
    public class InventoryCreateEvent : BaseEvent
    {
        public int? Id { get; set; }
        public int? Stock { get; set; }
        public int? ProductId { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}