namespace AppleShop.user.queryApplication.Queries.DTOs
{
    public class OrderItemDTO
    {
        public int? ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}