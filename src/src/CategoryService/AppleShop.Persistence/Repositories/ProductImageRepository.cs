using AppleShop.Domain.Abstractions.IRepositories;
using AppleShop.Domain.Entities;
using AppleShop.Persistence.Repositories.Base;

namespace AppleShop.Persistence.Repositories
{
    public class ProductImageRepository(ApplicationDbContext context) : GenericRepository<ProductImage>(context), IProductImageRepository
    {
        public void CreateRange(IEnumerable<ProductImage> productImages)
        {
            context.AddRange(productImages);
        }

    }
}