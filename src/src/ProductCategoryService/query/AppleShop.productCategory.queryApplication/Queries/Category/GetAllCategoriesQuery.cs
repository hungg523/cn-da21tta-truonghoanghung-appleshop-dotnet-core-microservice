using AppleShop.Share.Abstractions;
using Entities = AppleShop.productCategory.Domain.Entities;

namespace AppleShop.productCategory.queryApplication.Queries.Category
{
    public class GetAllCategoriesQuery : IQuery<List<Entities.Category>>
    {
    }
}