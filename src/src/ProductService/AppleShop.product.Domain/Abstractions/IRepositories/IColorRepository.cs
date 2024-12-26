using AppleShop.product.Domain.Abstractions.IRepositories.Base;
using AppleShop.product.Domain.Entities;

namespace AppleShop.product.Domain.Abstractions.IRepositories
{
    public interface IColorRepository : IGenericRepository<Color>
    {
        Task<(List<int>? existingIds, List<int>? missingIds)> CheckIdsExistAsync(List<int>? ids);
    }
}