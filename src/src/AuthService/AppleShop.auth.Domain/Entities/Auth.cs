using AppleShop.auth.Domain.Abstractions.Common;
using System.Text.Json.Serialization;

namespace AppleShop.auth.Domain.Entities
{
    public class Auth : BaseEntity
    {
        public int? Id { get; set; }
        public string? RefreshToken { get; set; }
        public int? UserId { get; set; }
        public int? IsActived { get; set; }
        public DateTime? IssuedAt { get; set; }
        public DateTime? Expiration { get; set; }
        public DateTime? RevokedAt { get; set; }
    }
}