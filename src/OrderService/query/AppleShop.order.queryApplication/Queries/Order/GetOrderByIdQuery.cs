using AppleShop.order.queryApplication.Queries.DTOs;
using AppleShop.Share.Abstractions;
using System.Text.Json.Serialization;

namespace AppleShop.order.queryApplication.Queries.Order
{
    public class GetOrderByIdQuery : IQuery<OrderFullDTO>
    {
        [JsonIgnore]
        public int? Id { get; set; }
    }
}