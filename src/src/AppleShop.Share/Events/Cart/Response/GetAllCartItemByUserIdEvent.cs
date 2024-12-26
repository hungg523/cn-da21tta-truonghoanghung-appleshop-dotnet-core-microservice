namespace AppleShop.Share.Events.Cart.Response
{
    public class GetAllCartItemByUserIdEvent
    {
        public List<int?> ProductIds { get; set; } = new List<int?>();
    }
}