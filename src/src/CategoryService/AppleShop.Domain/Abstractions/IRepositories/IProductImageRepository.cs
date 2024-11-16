using AppleShop.Domain.Abstractions.IRepositories.Base;
using AppleShop.Domain.Entities;

namespace AppleShop.Domain.Abstractions.IRepositories
{
    public interface IProductImageRepository : IGenericRepository<ProductImage>
    {
        public void CreateRange(IEnumerable<ProductImage> productImages);
    }
}