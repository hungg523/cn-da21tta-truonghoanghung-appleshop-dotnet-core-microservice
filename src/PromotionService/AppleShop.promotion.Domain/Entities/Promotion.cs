using AppleShop.promotion.Domain.Abstractions.Common;
using System.Text.Json.Serialization;

namespace AppleShop.promotion.Domain.Entities
{
    public class Promotion : BaseEntity
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
        public int? DiscountPercentage { get; set; }
        public int? TimesUsed { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}