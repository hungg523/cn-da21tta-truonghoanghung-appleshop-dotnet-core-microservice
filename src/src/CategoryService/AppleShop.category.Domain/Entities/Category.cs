using AppleShop.category.Domain.Abstractions.Common;

namespace AppleShop.category.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string? Name { get; set; }
        public string? Icon { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? IsActived { get; set; }
    }
}