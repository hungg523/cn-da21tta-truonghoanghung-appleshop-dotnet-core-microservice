using AppleShop.product.Domain.Abstractions.IRepositories.Base;
using AppleShop.product.Domain.Entities;

namespace AppleShop.product.Domain.Abstractions.IRepositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
    }
}