using AppleShop.Domain.Abstractions.IRepositories;
using AppleShop.Domain.Entities;
using AppleShop.Persistence.Repositories.Base;

namespace AppleShop.Persistence.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}