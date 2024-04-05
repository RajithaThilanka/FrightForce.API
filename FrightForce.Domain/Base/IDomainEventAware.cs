namespace FrightForce.Domain.Base;

public interface IDomainEventAware
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    void AddDomainEvent(IDomainEvent domainEvent);

    void RemoveDomainEvent(IDomainEvent domainEvent);
    IEvent[] ClearDomainEvents();
    void ClearDomainEventsSilent();
}