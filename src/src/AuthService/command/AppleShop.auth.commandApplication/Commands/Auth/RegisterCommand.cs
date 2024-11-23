using AppleShop.Share.Abstractions;

namespace AppleShop.auth.commandApplication.Commands.Auth
{
    public class RegisterCommand : ICommand
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}