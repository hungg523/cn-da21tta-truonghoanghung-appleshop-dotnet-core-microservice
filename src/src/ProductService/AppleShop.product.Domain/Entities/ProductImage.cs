using AppleShop.product.Domain.Abstractions.Common;

namespace AppleShop.product.Domain.Entities
{
    public class ProductImage : BaseEntity
    {
        public string? Title { get; set; }
        public string? Url { get; set; }
        public int? Position { get; set; }
        public int? ProductId { get; set; }
    }
}