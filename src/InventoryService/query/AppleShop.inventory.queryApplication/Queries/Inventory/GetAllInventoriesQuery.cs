using AppleShop.Share.Abstractions;
using Entities = AppleShop.inventory.Domain.Entities;

namespace AppleShop.inventory.queryApplication.Queries.Inventory
{
    public class GetAllInventoriesQuery : IQuery<List<Entities.Inventory>>
    {
    }
}