using AppleShop.auth.commandApplication.Commands.DTOs;
using AppleShop.Share.Abstractions;

namespace AppleShop.auth.commandApplication.Commands.Auth
{
    public class RefreshTokenCommand : ICommand<LoginDTO>
    {
        public string? RefreshToken { get; set; }
    }
}