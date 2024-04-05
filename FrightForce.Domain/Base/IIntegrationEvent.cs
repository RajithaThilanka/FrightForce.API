namespace FrightForce.Domain.Base;

public interface IIntegrationEvent : IEvent
{
    Guid EventId => Guid.NewGuid();
    public DateTime OccurredOn => DateTime.Now;
    public string EventType => GetType().AssemblyQualifiedName;
}