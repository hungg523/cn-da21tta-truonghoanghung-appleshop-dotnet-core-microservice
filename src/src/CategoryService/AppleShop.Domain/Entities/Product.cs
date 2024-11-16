using AppleShop.Domain.Abstractions.Common;
using System.Text.Json.Serialization;

namespace AppleShop.Domain.Entities
{
    public class Product : BaseEntity
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? StockQuantity { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CategoryId { get; set; }
        public int? IsActived { get; set; }
    }
}