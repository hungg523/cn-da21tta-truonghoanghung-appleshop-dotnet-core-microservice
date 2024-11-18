using AppleShop.productCategory.Domain.Abstractions.IRepositories.Base;
using AppleShop.productCategory.Domain.Entities;

namespace AppleShop.productCategory.Domain.Abstractions.IRepositories
{
    public interface IColorRepository : IGenericRepository<Color>
    {
        Task<(List<int>? existingIds, List<int>? missingIds)> CheckIdsExistAsync(List<int>? ids);
    }
}