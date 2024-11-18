namespace AppleShop.Share.Abstractions
{
    public interface IShareEventDispatcher
    {
        Task PublishAsync<T>(T domainEvent, bool toOutboxWhenFail = false, CancellationToken? cancellationToken = null) where T : IDomainEvent;
    }
}