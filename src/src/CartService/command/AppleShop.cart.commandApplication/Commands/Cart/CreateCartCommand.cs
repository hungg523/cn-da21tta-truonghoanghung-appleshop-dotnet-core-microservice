using AppleShop.cart.commandApplication.Commands.DTOs;
using AppleShop.Share.Abstractions;
using System.Text.Json.Serialization;

namespace AppleShop.cart.commandApplication.Commands.Category
{
    public class CreateCartCommand : ICommand
    {
        public int? UserId { get; set; }
        public List<CartItemDTO>? CartItems { get; set; } = new List<CartItemDTO>();
    }
}