using AppleShop.product.Domain.Abstractions.IRepositories.Base;
using AppleShop.product.Domain.Entities;

namespace AppleShop.product.Domain.Abstractions.IRepositories
{
    public interface IProductImageRepository : IGenericRepository<ProductImage>
    {
        public void CreateRange(IEnumerable<ProductImage> productImages);
    }
}