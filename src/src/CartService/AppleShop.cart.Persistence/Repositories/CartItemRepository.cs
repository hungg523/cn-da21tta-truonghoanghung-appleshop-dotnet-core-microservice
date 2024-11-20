using AppleShop.cart.Domain.Abstractions.IRepositories;
using AppleShop.cart.Domain.Entities;
using AppleShop.cart.Persistence.Repositories.Base;

namespace AppleShop.cart.Persistence.Repositories
{
    public class CartItemRepository(ApplicationDbContext context) : GenericRepository<CartItem>(context), ICartItemRepository
    {
    }
}