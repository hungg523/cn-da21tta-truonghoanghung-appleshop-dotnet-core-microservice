using AppleShop.product.queryApplication.Queries.DTOs;
using AppleShop.Share.Abstractions;

namespace AppleShop.product.queryApplication.Queries.Product
{
    public class GetProductByIdQuery : IQuery<ProductFullDTO>
    {
        public int? Id { get; set; }
    }
}