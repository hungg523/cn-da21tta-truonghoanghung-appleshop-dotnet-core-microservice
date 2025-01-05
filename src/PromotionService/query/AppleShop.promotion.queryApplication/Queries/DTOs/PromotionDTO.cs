using AppleShop.Share.Enumerations;

namespace AppleShop.promotion.queryApplication.Queries.DTOs
{
    public class PromotionDTO
    {
        public int? Id { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public int? DiscountPercentage { get; set; }
        public int? TimesUsed { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}