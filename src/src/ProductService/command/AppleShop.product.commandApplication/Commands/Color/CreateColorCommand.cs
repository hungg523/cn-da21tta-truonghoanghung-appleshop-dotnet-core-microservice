using AppleShop.Share.Abstractions;

namespace AppleShop.product.commandApplication.Commands.Color
{
    public class CreateColorCommand : ICommand
    {
        public string? Name { get; set; }
    }
}