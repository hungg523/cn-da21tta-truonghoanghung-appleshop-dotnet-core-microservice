using AppleShop.category.Domain.Abstractions.IRepositories;
using AppleShop.category.Domain.Entities;
using AppleShop.category.Persistence.Repositories.Base;

namespace AppleShop.category.Persistence.Repositories
{
    public class CategoryRepository(ApplicationDbContext context) : GenericRepository<Category>(context), ICategoryRepository
    {
    }
}