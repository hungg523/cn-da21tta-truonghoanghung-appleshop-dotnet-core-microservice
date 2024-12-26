using AppleShop.Share.Enumerations;

namespace AppleShop.order.queryApplication.Queries.DTOs
{
    public class OrderFullDTO
    {
        public int? Id { get; set; }
        public ICollection<UserDTO>? Users { get; set; } = new List<UserDTO>();
        public string? Status { get; set; }
        public ICollection<UserAddressDTO>? UserAddresses { get; set; } = new List<UserAddressDTO>();
        public ICollection<PromotionDTO>? Promotions { get; set; } = new List<PromotionDTO>();
        public ICollection<OrderItemDTO>? OrderItems { get; set; } = new List<OrderItemDTO>();
        public decimal? TotalAmount { get; set; }
    }
}