using AppleShop.product.queryApplication.Queries.DTOs;
using AppleShop.Share.Abstractions;

namespace AppleShop.product.queryApplication.Queries.Product
{
    public class GetAllProductsActivedByNameQuery : IQuery<List<ProductFullDTO>>
    {
        public string? Name { get; set; }
    }
}