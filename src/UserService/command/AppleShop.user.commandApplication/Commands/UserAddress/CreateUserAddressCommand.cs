using AppleShop.Share.Abstractions;

namespace AppleShop.user.commandApplication.Commands.UserAddress
{
    public class CreateUserAddressCommand : ICommand
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AddressLine { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Province { get; set; }
        public string? District { get; set; }
        public int? UserId { get; set; }
    }
}