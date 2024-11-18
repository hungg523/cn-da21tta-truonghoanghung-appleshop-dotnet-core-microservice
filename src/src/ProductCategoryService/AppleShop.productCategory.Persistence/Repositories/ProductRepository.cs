using AppleShop.productCategory.Domain.Abstractions.IRepositories;
using AppleShop.productCategory.Domain.Entities;
using AppleShop.productCategory.Persistence.Repositories.Base;

namespace AppleShop.productCategory.Persistence.Repositories
{
    public class ProductRepository(ApplicationDbContext context) : GenericRepository<Product>(context), IProductRepository
    {
    }
}