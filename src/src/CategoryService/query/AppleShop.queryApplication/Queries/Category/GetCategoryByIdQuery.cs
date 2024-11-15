using Entities = AppleShop.Domain.Entities;
using AppleShop.Share.Abstractions;

namespace AppleShop.queryApplication.Queries.Category
{
    public class GetCategoryByIdQuery : IQuery<Entities.Category>
    {
        public int? Id { get; set; }
    }
}