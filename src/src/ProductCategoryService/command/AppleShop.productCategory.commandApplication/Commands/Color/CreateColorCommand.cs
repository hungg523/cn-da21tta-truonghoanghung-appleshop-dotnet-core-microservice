using AppleShop.Share.Abstractions;

namespace AppleShop.productCategory.commandApplication.Commands.Color
{
    public class CreateColorCommand : ICommand
    {
        public string? Name { get; set; }
    }
}