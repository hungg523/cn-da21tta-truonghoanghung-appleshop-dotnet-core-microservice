using AppleShop.order.Domain.Abstractions.IRepositories;
using AppleShop.order.Domain.Entities;
using AppleShop.order.Persistence.Repositories.Base;

namespace AppleShop.order.Persistence.Repositories
{
    public class OrderRepository(ApplicationDbContext context) : GenericRepository<Order>(context), IOrderRepository
    {
    }
}