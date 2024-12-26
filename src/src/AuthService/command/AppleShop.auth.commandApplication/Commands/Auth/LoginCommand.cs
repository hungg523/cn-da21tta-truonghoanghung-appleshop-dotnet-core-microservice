using AppleShop.auth.commandApplication.Commands.DTOs;
using AppleShop.Share.Abstractions;
using System.Text.Json.Serialization;

namespace AppleShop.auth.commandApplication.Commands.Auth
{
    public class LoginCommand : ICommand<LoginDTO>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}