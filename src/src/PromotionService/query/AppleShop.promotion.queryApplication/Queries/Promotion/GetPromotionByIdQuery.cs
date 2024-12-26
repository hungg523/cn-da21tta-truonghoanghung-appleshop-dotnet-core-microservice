using AppleShop.promotion.queryApplication.Queries.DTOs;
using AppleShop.Share.Abstractions;

namespace AppleShop.promotion.queryApplication.Queries.Promotion
{
    public class GetPromotionByIdQuery : IQuery<PromotionDTO>
    {
        public int? Id { get; set; }
    }
}