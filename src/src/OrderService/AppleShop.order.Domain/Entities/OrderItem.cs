using AppleShop.order.Domain.Abstractions.Common;

namespace AppleShop.order.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public int? Id { get; set; }
        public int?OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }
        public Order Order { get; set; }
    }
}