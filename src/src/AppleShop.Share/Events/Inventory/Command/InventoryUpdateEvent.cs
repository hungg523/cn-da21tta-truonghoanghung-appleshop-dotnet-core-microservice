using AppleShop.Share.Events.Common;
using System.Text.Json.Serialization;

namespace AppleShop.Share.Events.Inventory.Command
{
    public class InventoryUpdateEvent : BaseEvent
    {
        public int? Id { get; set; }
        public int? Stock { get; set; }
        public int? ProductId { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}