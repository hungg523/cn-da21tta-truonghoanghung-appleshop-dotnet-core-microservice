using AppleShop.Share.Abstractions;
using System.Text.Json.Serialization;

namespace AppleShop.user.commandApplication.Commands.User
{
    public class UpdateProfileUserCommand : ICommand
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public string? UserName { get; set; }
        public string? ImageData { get; set; }
    }
}