using AppleShop.Share.Abstractions;

namespace AppleShop.auth.commandApplication.Commands.Auth
{
    public class ResetPasswordCommand : ICommand
    {
        public string? Email { get; set; }
    }
}