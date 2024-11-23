using AppleShop.order.Domain.Abstractions.Common;
using System.Text.Json.Serialization;

namespace AppleShop.order.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int? Id { get; set; }
        public string? OrderStatus { get; set; }
        public string? Payment { get; set; }
        public int? UserId { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? PromotionId { get; set; }
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}