namespace AppleShop.auth.commandApplication.Commands.DTOs
{
    public class LoginDTO
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}