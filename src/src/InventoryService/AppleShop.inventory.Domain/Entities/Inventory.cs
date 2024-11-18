using AppleShop.inventory.Domain.Abstractions.Common;

namespace AppleShop.inventory.Domain.Entities
{
    public class Inventory : BaseEntity
    {
        public int? Id { get; set; }
        public int? Stock { get; set; }
        public int? ProductId { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}