using AppleShop.Share.Abstractions;

namespace AppleShop.productCategory.commandApplication.Commands.Color
{
    public class DeleteColorCommand : ICommand
    {
        public int? Id { get; set; }
    }
}