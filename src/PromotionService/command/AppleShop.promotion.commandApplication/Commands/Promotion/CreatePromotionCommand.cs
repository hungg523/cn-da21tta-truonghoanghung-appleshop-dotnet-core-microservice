using AppleShop.Share.Abstractions;
using System.Text.Json.Serialization;

namespace AppleShop.promotion.commandApplication.Commands.Promotion
{
    public class CreatePromotionCommand : ICommand
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
        public int? DiscountPercentage { get; set; } = 0;
        public int? TimesUsed { get; set; } = 0;
        public DateTime? StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; } = DateTime.Now;

        [JsonIgnore]
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}