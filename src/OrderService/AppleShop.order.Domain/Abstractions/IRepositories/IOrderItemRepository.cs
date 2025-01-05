using AppleShop.order.Domain.Abstractions.IRepositories.Base;
using AppleShop.order.Domain.Entities;

namespace AppleShop.order.Domain.Abstractions.IRepositories
{
    public interface IOrderItemRepository : IGenericRepository<OrderItem>
    {
    }
}