namespace FrightForce.Domain.Base;

public interface IDomainEventDispatcher
{
    public Task Dispatch(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
    public Task Dispatch(IEnumerable<IDomainEvent> domainEvent, CancellationToken cancellationToken = default);
}