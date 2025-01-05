using AppleShop.order.Domain.Entities;
using AppleShop.Share.Abstractions;

namespace AppleShop.order.queryApplication.Queries.Order
{
    public class GetAllItemsByOrderIdQuery : IQuery<List<OrderItem>>
    {
        public int? OrderId { get; set; }
    }
}