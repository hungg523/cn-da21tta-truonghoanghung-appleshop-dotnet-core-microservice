using AppleShop.Share.Events.Inventory.Query;

namespace AppleShop.Share.Events.Cart.Query
{
    public class ProductsResponse
    {
        public ICollection<ProductResponse>? Products { get; set; } = new List<ProductResponse>();
    }
}