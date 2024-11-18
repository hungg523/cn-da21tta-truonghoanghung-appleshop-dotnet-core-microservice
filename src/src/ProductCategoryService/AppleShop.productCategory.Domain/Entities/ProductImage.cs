using AppleShop.productCategory.Domain.Abstractions.Common;

namespace AppleShop.productCategory.Domain.Entities
{
    public class ProductImage : BaseEntity
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }
        public int? Position { get; set; }
        public int? ProductId { get; set; }
    }
}