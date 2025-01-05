using AppleShop.product.Domain.Abstractions.Common;
using System.Text.Json.Serialization;

namespace AppleShop.product.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int? SoldQuantity { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CategoryId { get; set; }
        public int? IsActived { get; set; }

        [JsonIgnore]
        public ICollection<ProductColor>? ProductColors { get; set; }
    }
}