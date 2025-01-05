using AppleShop.Share.Abstractions;

namespace AppleShop.order.commandApplication.Commands.Order
{
    public class CancelOrderCommand : ICommand
    {
        public int? UserId { get; set; }
        public int? OrderId { get; set; }
    }
}