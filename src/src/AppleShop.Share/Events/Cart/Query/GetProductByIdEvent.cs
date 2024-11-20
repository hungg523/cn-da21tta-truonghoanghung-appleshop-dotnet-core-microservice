using AppleShop.Share.Events.Common;

namespace AppleShop.Share.Events.Cart.Query
{
    public class GetProductByIdEvent : BaseEvent
    {
        public int? ProductId { get; set; }
    }
}