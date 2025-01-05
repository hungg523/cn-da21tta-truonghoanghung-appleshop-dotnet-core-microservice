using AppleShop.cart.Domain.Abstractions.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AppleShop.cart.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public int? UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public ICollection<CartItem>? CartItems { get; set; }
    }
}