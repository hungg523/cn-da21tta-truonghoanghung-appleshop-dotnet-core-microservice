using Entities = AppleShop.productCategory.Domain.Entities;
using AppleShop.Share.Abstractions;

namespace AppleShop.productCategory.queryApplication.Queries.Category
{
    public class GetCategoryByIdQuery : IQuery<Entities.Category>
    {
        public int? Id { get; set; }
    }
}