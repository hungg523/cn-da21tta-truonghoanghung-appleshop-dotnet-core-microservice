using AppleShop.Share.Abstractions;

namespace AppleShop.product.commandApplication.Commands.ProductImage
{
    public class CreateProductImageCommand : ICommand
    {
        public string? Title { get; set; }
        public string? ImageData { get; set; }
        public int? Position { get; set; } = 0;
        public int? ProductId { get; set; }
    }
}