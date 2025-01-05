namespace AppleShop.Share.Events.Cart.Response
{
    public class ProductsResponse
    {
        public ICollection<ProductResponse>? Products { get; set; } = new List<ProductResponse>();
    }
}