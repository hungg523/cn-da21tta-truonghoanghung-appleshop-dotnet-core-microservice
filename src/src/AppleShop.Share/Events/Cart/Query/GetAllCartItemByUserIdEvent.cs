namespace AppleShop.Share.Events.Cart.Query
{
    public class GetAllCartItemByUserIdEvent
    {
        public List<int?> ProductIds { get; set; } = new List<int?>();
    }
}