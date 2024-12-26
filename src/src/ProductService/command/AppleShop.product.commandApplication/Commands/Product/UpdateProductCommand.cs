using AppleShop.Share.Abstractions;
using System.Text.Json.Serialization;

namespace AppleShop.product.commandApplication.Commands.Product
{
    public class UpdateProductCommand : ICommand
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<int>? ColorIds { get; set; } = new List<int>();
        public decimal? Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int? StockQuantity { get; set; }
        public int? CategoryId { get; set; }
        public int? IsActived { get; set; }
    }
}