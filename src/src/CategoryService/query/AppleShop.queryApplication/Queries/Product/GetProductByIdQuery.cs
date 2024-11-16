using AppleShop.queryApplication.Queries.DTOs;
using AppleShop.Share.Abstractions;

namespace AppleShop.queryApplication.Queries.Product
{
    public class GetProductByIdQuery : IQuery<ProductFullDTO>
    {
        public int? Id { get; set; }
    }
}