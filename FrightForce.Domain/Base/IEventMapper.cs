namespace FrightForce.Domain.Base;

public interface IEventMapper
{
    IIntegrationEvent? Map(IDomainEvent @event);
    IEnumerable<IIntegrationEvent> MapAll(IEnumerable<IDomainEvent> events);
}