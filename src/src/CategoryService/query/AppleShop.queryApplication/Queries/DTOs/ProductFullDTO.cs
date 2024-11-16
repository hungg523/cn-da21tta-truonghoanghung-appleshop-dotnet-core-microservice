namespace AppleShop.queryApplication.Queries.DTOs
{
    public class ProductFullDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? StockQuantity { get; set; }
        public int? IsActived { get; set; }
        public ICollection<ProductImageDTO>? Images { get; set; } = new List<ProductImageDTO>();
    }
}