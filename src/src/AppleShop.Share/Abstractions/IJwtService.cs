using System.Security.Claims;

namespace AppleShop.Share.Abstractions
{
    public interface IJwtService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
    }
}