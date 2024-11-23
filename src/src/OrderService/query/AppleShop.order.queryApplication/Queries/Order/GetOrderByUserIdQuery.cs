using AppleShop.order.queryApplication.Queries.DTOs;
using AppleShop.Share.Abstractions;

namespace AppleShop.order.queryApplication.Queries.Order
{
    public class GetOrderByUserIdQuery : IQuery<List<OrderFullDTO>>
    {
        public int? UserId { get; set; }
    }
}