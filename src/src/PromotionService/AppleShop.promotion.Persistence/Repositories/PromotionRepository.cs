using AppleShop.promotion.Domain.Abstractions.IRepositories;
using AppleShop.promotion.Domain.Entities;
using AppleShop.promotion.Persistence.Repositories.Base;

namespace AppleShop.promotion.Persistence.Repositories
{
    public class PromotionRepository(ApplicationDbContext context) : GenericRepository<Promotion>(context), IPromotionRepository
    {
    }
}