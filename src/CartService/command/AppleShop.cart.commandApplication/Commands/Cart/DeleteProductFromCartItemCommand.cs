using AppleShop.Share.Abstractions;
using System.Text.Json.Serialization;

namespace AppleShop.cart.commandApplication.Commands.Cart
{
    public class DeleteProductFromCartItemCommand : ICommand
    {
        [JsonIgnore]
        public int? CartId { get; set; }

        [JsonIgnore]
        public int? ProductId { get; set; }
    }
}