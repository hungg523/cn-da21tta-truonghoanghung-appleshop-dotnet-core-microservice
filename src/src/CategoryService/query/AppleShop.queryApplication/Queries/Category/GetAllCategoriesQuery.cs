using AppleShop.Share.Abstractions;
using Entities = AppleShop.Domain.Entities;

namespace AppleShop.queryApplication.Queries.Category
{
    public class GetAllCategoriesQuery : IQuery<List<Entities.Category>>
    {
    }
}