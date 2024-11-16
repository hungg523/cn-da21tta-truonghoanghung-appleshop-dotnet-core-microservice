using AppleShop.Share.Abstractions;

namespace AppleShop.commandApplication.Commands.ProductImage
{
    public class DeleteProductImageCommand : ICommand
    {
        public int? Id { get; set; }
    }
}