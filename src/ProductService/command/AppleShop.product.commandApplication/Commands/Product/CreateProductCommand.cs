using AppleShop.Share.Abstractions;
using System.Text.Json.Serialization;

namespace AppleShop.product.commandApplication.Commands.Product
{
    public class CreateProductCommand : ICommand
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<int>? ColorIds { get; set; } = new List<int>();
        public decimal? Price { get; set; } = 0;
        public decimal? DiscountPrice { get; set; } = 0;
        
        [JsonIgnore]
        public int? SoldQuantity { get; set; } = 0;
        public int? StockQuantity { get; set; } = 0;

        [JsonIgnore]
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public int? CategoryId { get; set; }
        public int? IsActived { get; set; } = 0;
    }
}