using AppleShop.Share.Abstractions;

namespace AppleShop.Domain.Events.Common
{
    public class BaseEvent : IDomainEvent
    {
        public DateTime DateOccurred => DateTime.Now;
    }
}