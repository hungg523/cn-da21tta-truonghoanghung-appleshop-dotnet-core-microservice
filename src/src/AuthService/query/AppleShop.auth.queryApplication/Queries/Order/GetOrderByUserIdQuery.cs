using AppleShop.auth.queryApplication.Queries.DTOs;
using AppleShop.Share.Abstractions;

namespace AppleShop.auth.queryApplication.Queries.Order
{
    public class GetOrderByUserIdQuery : IQuery<List<OrderFullDTO>>
    {
        public int? UserId { get; set; }
    }
}