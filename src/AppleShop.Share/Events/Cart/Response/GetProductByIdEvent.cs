using AppleShop.Share.Events.Common;

namespace AppleShop.Share.Events.Cart.Response
{
    public class GetProductByIdEvent : BaseEvent
    {
        public int? ProductId { get; set; }
    }
}