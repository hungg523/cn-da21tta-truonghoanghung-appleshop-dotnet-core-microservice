using AppleShop.productCategory.queryApplication.Queries.DTOs;
using AppleShop.Share.Abstractions;

namespace AppleShop.productCategory.queryApplication.Queries.Product
{
    public class GetAllProductByCategoryIdQuery : IQuery<List<ProductFullDTO>>
    {
        public int? CategoryId { get; set; }
    }
}