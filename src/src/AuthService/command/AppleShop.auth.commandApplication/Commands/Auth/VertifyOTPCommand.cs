using AppleShop.Share.Abstractions;
using System.Text.Json.Serialization;

namespace AppleShop.auth.commandApplication.Commands.Auth
{
    public class VertifyOTPCommand : ICommand
    {
        public string? Email { get; set; }
        public string? OTP { get; set; }
    }
}