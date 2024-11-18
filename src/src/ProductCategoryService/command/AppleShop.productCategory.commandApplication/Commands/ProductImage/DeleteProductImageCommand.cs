using AppleShop.Share.Abstractions;

namespace AppleShop.productCategory.commandApplication.Commands.ProductImage
{
    public class DeleteProductImageCommand : ICommand
    {
        public int? Id { get; set; }
    }
}