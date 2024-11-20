using AppleShop.productCategory.Domain.Abstractions.Common;
using System.Text.Json.Serialization;

namespace AppleShop.productCategory.Domain.Entities
{
    public class Color : BaseEntity
    {
        public int? Id { get; set; }
        public string? Name { get; set; }

        [JsonIgnore]
        public ICollection<ProductColor>? ProductColors { get; set; }
    }
}