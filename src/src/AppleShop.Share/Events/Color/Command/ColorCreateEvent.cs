using AppleShop.Share.Events.Common;

namespace AppleShop.Share.Events.Color.Command
{
    public class ColorCreateEvent : BaseEvent
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? ProductId { get; set; }
    }
}