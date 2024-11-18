using AppleShop.productCategory.Domain.Abstractions.IRepositories.Base;
using AppleShop.productCategory.Domain.Entities;

namespace AppleShop.productCategory.Domain.Abstractions.IRepositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
    }
}