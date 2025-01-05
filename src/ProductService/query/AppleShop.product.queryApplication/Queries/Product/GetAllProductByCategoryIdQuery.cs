using AppleShop.product.queryApplication.Queries.DTOs;
using AppleShop.Share.Abstractions;

namespace AppleShop.product.queryApplication.Queries.Product
{
    public class GetAllProductByCategoryIdQuery : IQuery<List<ProductFullDTO>>
    {
        public int? CategoryId { get; set; }
    }
}