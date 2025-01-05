using AppleShop.product.Domain.Abstractions.Common;
using System.Text.Json.Serialization;

namespace AppleShop.product.Domain.Entities
{
    public class Color : BaseEntity
    {
        public string? Name { get; set; }

        [JsonIgnore]
        public ICollection<ProductColor>? ProductColors { get; set; }
    }
}