using AppleShop.Share.Abstractions;
using System.Text.Json.Serialization;

namespace AppleShop.user.commandApplication.Commands.UserAddress
{
    public class UpdateUserAddressCommand : ICommand
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AddressLine { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Province { get; set; }
        public string? District { get; set; }
    }
}