using Entities = AppleShop.category.Domain.Entities;
using AppleShop.Share.Abstractions;

namespace AppleShop.category.queryApplication.Queries.Category
{
    public class GetCategoryByIdQuery : IQuery<Entities.Category>
    {
        public int? Id { get; set; }
    }
}