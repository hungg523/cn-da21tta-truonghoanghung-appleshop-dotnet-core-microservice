using AppleShop.productCategory.Domain.Abstractions.Common;

namespace AppleShop.productCategory.Domain.Entities
{
    public class Category : BaseEntity
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Icon { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? IsActived { get; set; }
    }
}