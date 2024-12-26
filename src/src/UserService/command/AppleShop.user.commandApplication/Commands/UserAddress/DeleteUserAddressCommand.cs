using AppleShop.Share.Abstractions;

namespace AppleShop.user.commandApplication.Commands.UserAddress
{
    public class DeleteUserAddressCommand : ICommand
    {
        public int? Id { get; set; }
    }
}