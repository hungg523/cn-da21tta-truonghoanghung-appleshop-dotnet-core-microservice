using AppleShop.queryApplication.Queries.DTOs;
using AppleShop.Share.Abstractions;

namespace AppleShop.queryApplication.Queries.Product
{
    public class GetAllProductByCategoryIdQuery : IQuery<List<ProductFullDTO>>
    {
        public int? CategoryId { get; set; }
    }
}