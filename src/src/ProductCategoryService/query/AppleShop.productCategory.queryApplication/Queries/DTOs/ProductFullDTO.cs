namespace AppleShop.productCategory.queryApplication.Queries.DTOs
{
    public class ProductFullDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<string>? Colors { get; set; } = new List<string>();
        public decimal? Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int? Stock { get; set; }
        public int? IsActived { get; set; }
        public ICollection<ProductImageDTO>? Images { get; set; } = new List<ProductImageDTO>();
    }
}