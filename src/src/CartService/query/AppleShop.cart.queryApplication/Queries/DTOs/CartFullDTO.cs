namespace AppleShop.cart.queryApplication.Queries.DTOs
{
    public class CartFullDTO
    {
        public int? Id { get; set; }
        public ICollection<CartItemDTO>? CartItems { get; set; } = new List<CartItemDTO>();
        public decimal? TotalPrice { get; set; }
    }
}