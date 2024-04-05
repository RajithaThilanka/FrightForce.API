namespace FrightForce.Domain.Base;

public class AuditableDomainEntity<TKey> : BaseAuditableEntity<TKey>, IDomainEventAware
{
    protected readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public IEvent[] ClearDomainEvents()
    {
        IEvent[] dequeuedEvents = _domainEvents.ToArray();
        _domainEvents.Clear();
        return dequeuedEvents;
    }

    public void ClearDomainEventsSilent()
    {
        _domainEvents.Clear();
    }
}