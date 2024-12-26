using AppleShop.Share.Events.Common;

namespace AppleShop.Share.Events.Order.Request
{
    public class UpdateSoldQuantityEvent : BaseEvent
    {
         public Dictionary<int, int> SoldQuantity {  get; set; }
    }
}