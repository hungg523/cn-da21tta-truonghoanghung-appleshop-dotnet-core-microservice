using AppleShop.productCategory.queryApplication.Queries.DTOs;
using AppleShop.Share.Abstractions;

namespace AppleShop.productCategory.queryApplication.Queries.Product
{
    public class GetProductByIdQuery : IQuery<ProductFullDTO>
    {
        public int? Id { get; set; }
    }
}