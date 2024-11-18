using AppleShop.productCategory.Domain.Abstractions.Common;

namespace AppleShop.productCategory.Domain.Entities
{
    public class ProductColor : BaseEntity
    {
        public int? ColorId { get; set; }
        public int? ProductId { get; set; }
    }
}