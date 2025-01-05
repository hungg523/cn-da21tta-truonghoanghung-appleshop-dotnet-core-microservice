using Entities = AppleShop.inventory.Domain.Entities;
using AppleShop.Share.Abstractions;

namespace AppleShop.inventory.queryApplication.Queries.Inventory
{
    public class GetInventoryByIdQuery : IQuery<Entities.Inventory>
    {
        public int? Id { get; set; }
    }
}