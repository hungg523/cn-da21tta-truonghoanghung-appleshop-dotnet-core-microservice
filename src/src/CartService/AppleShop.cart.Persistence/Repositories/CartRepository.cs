using AppleShop.cart.Domain.Abstractions.IRepositories;
using AppleShop.cart.Domain.Entities;
using AppleShop.cart.Persistence.Repositories.Base;

namespace AppleShop.cart.Persistence.Repositories
{
    public class CartRepository(ApplicationDbContext context) : GenericRepository<Cart>(context), ICartRepository
    {
    }
}