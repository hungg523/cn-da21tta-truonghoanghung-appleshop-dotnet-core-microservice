using AppleShop.product.Domain.Abstractions.IRepositories;
using AppleShop.product.Domain.Entities;
using AppleShop.product.Persistence.Repositories.Base;

namespace AppleShop.product.Persistence.Repositories
{
    public class ProductImageRepository(ApplicationDbContext context) : GenericRepository<ProductImage>(context), IProductImageRepository
    {
        public void CreateRange(IEnumerable<ProductImage> productImages)
        {
            context.AddRange(productImages);
        }

    }
}