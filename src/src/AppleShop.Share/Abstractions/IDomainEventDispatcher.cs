namespace AppleShop.Share.Abstractions
{
    public interface IDomainEventDispatcher
    {
        Task PublishAsync<T>(T domainEvent, bool toOutboxWhenFail = false, CancellationToken? cancellationToken = null) where T : IDomainEvent;
    }
}