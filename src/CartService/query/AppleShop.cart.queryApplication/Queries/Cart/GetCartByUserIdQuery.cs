using AppleShop.cart.queryApplication.Queries.DTOs;
using AppleShop.Share.Abstractions;

namespace AppleShop.cart.queryApplication.Queries.Cart
{
    public class GetCartByUserIdQuery : IQuery<CartFullDTO>
    {
        public int? UserId { get; set; }
    }
}