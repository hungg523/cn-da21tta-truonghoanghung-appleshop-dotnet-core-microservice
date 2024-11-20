using AppleShop.cart.Domain.Abstractions.IRepositories.Base;
using AppleShop.cart.Domain.Entities;

namespace AppleShop.cart.Domain.Abstractions.IRepositories
{
    public interface ICartItemRepository : IGenericRepository<CartItem>
    {
    }
}