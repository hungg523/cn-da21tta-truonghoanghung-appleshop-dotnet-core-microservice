using AppleShop.Share.Abstractions;
using Entities = AppleShop.category.Domain.Entities;

namespace AppleShop.category.queryApplication.Queries.Category
{
    public class GetAllCategoriesActivedQuery : IQuery<List<Entities.Category>>
    {
    }
}