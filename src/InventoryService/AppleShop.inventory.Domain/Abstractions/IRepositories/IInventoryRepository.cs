using AppleShop.inventory.Domain.Abstractions.IRepositories.Base;
using AppleShop.inventory.Domain.Entities;

namespace AppleShop.inventory.Domain.Abstractions.IRepositories
{
    public interface IInventoryRepository : IGenericRepository<Inventory>
    {
    }
}