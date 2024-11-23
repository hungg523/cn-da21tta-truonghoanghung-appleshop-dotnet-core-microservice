using AppleShop.Share.Abstractions;
using System.Text.Json.Serialization;

namespace AppleShop.order.commandApplication.Commands.Order
{
    public class ChangeOrderStatusCommand : ICommand
    {
        [JsonIgnore]
        public int? OrderId { get; set; }
        public int? OrderStatus { get; set; }
    }
}