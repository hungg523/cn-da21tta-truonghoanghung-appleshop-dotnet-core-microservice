using AppleShop.productCategory.Domain.Abstractions.IRepositories;
using AppleShop.productCategory.Domain.Entities;
using AppleShop.productCategory.Persistence.Repositories.Base;
using AppleShop.Share.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AppleShop.productCategory.Persistence.Repositories
{
    public class ColorRepository(ApplicationDbContext context) : GenericRepository<Color>(context), IColorRepository
    {
        public async Task<(List<int>? existingIds, List<int>? missingIds)> CheckIdsExistAsync(List<int>? ids)
        {
            ids = ids.Distinct().ToList() ?? new List<int>();
            var existingIds = await context.Set<Color>()
                                   .Where(color => color.Id.HasValue && ids.Contains(color.Id.Value))
                                   .Select(color => color.Id.Value)
                                   .ToListAsync();
            var missingIds = ids.Except(existingIds).ToList();
            if (missingIds.Any()) AppleException.ThrowNotFound(message: $"Id {string.Join(", ", missingIds)} is not found in Color");
            return (existingIds, missingIds);
        }
    }
}