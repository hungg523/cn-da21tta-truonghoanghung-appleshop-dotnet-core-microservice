using AppleShop.Share.Abstractions;
using AppleShop.Share.Outbox;

namespace obsCommerce.Contract.Abstractions
{
    public interface IOutboxHandler
    {
        OutboxMessage ConvertToOutboxMessage(IDomainEvent domainEvent);
        Task SaveOutboxMessageAsync(OutboxMessage message);
    }
}