using AppleShop.productCategory.Domain.Abstractions.IRepositories;
using AppleShop.productCategory.Domain.Entities;
using AppleShop.productCategory.Persistence.Repositories.Base;

namespace AppleShop.productCategory.Persistence.Repositories
{
    public class ProductImageRepository(ApplicationDbContext context) : GenericRepository<ProductImage>(context), IProductImageRepository
    {
        public void CreateRange(IEnumerable<ProductImage> productImages)
        {
            context.AddRange(productImages);
        }

    }
}