using AppleShop.productCategory.Domain.Abstractions.IRepositories.Base;
using AppleShop.productCategory.Domain.Entities;

namespace AppleShop.productCategory.Domain.Abstractions.IRepositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
    }
}