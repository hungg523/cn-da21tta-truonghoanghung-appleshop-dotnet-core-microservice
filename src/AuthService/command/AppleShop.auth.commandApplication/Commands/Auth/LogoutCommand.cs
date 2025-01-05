using AppleShop.Share.Abstractions;

namespace AppleShop.auth.commandApplication.Commands.Auth
{
    public class LogoutCommand : ICommand
    {
        public string? Email { get; set; }
    }
}