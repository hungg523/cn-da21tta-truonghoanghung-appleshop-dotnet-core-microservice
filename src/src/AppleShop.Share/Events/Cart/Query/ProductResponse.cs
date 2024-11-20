namespace AppleShop.Share.Events.Cart.Query
{
    public class ProductResponse
    {
        public int? ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountPrice { get; set; }
    }
}