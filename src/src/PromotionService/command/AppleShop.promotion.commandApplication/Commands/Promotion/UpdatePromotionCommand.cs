using AppleShop.Share.Abstractions;
using System.Text.Json.Serialization;

namespace AppleShop.promotion.commandApplication.Commands.Promotion
{
    public class UpdatePromotionCommand : ICommand
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public int? DiscountPercentage { get; set; }
        public int? TimesUsed { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}