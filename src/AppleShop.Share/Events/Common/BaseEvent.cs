using AppleShop.Share.Abstractions;

namespace AppleShop.Share.Events.Common
{
    public class BaseEvent : IDomainEvent
    {
        public DateTime DateOccurred => DateTime.Now;
    }
}