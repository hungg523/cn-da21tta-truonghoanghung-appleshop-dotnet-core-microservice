using AppleShop.Share.Abstractions;

namespace AppleShop.product.commandApplication.Commands.ProductImage
{
    public class DeleteProductImageCommand : ICommand
    {
        public int? Id { get; set; }
    }
}