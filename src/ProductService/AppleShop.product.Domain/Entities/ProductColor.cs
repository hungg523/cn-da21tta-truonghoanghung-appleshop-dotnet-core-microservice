using AppleShop.product.Domain.Abstractions.Common;

namespace AppleShop.product.Domain.Entities
{
    public class ProductColor : BaseEntity
    {
        public int? ColorId { get; set; }
        public int? ProductId { get; set; }
    }
}