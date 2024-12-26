using AppleShop.order.commandApplication.Commands.DTOs;
using AppleShop.Share.Abstractions;

namespace AppleShop.order.commandApplication.Commands.Order
{
    public class CreateOrderCommand : ICommand
    {
        public int? UserId { get; set; }
        public int? UserAddressId { get; set; }
        public string? PromotionCode { get; set; }
        public List<OrderItemDTO>? OrderItems { get; set; } = new List<OrderItemDTO>();
    }
}