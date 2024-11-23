using AppleShop.Share.Enumerations;

namespace AppleShop.user.queryApplication.Queries.DTOs
{
    public class OrderFullDTO
    {
        public int? Id { get; set; }
        public string? Status { get; set; }
        public ICollection<OrderItemDTO>? OrderItems { get; set; } = new List<OrderItemDTO>();
        public decimal? TotalAmount { get; set; }
    }
}