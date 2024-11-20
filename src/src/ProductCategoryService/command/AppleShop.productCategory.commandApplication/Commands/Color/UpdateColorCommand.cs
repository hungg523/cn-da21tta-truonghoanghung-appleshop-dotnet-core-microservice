using AppleShop.Share.Abstractions;

namespace AppleShop.productCategory.commandApplication.Commands.Color
{
    public class UpdateColorCommand : ICommand
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
    }
}