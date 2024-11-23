using AppleShop.Share.Abstractions;

namespace AppleShop.auth.commandApplication.Commands.Auth
{
    public class ResendOTPCommand : ICommand
    {
        public string? Email { get; set; }
    }
}