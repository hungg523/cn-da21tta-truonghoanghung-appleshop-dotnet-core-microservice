using AppleShop.Share.Abstractions;

namespace AppleShop.product.commandApplication.Commands.Color
{
    public class DeleteColorCommand : ICommand
    {
        public int? Id { get; set; }
    }
}