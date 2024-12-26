using AppleShop.Share.Abstractions;
using System.Text.Json.Serialization;

namespace AppleShop.auth.commandApplication.Commands.Auth
{
    public class ChangePasswordCommand : ICommand
    {
        [JsonIgnore]
        public string? Email { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}