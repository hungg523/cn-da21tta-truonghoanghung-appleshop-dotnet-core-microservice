using AppleShop.Domain.Abstractions.IRepositories;
using AppleShop.Domain.Entities;
using AppleShop.Persistence.Repositories.Base;

namespace AppleShop.Persistence.Repositories
{
    public class CategoryRepository(ApplicationDbContext context) : GenericRepository<Category>(context), ICategoryRepository
    {
    }
}