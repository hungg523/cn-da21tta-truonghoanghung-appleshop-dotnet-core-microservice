using AppleShop.inventory.Domain.Abstractions.IRepositories;
using AppleShop.inventory.Domain.Entities;
using AppleShop.inventory.Persistence.Repositories.Base;

namespace AppleShop.inventory.Persistence.Repositories
{
    public class InventoryRepository(ApplicationDbContext context) : GenericRepository<Inventory>(context), IInventoryRepository
    {
    }
}