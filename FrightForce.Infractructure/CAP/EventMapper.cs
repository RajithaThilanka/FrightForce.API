using FrightForce.Domain.Base;

namespace FrightForce.Infractructure.CAP;

public class EventMapper : IEventMapper
{
    public IIntegrationEvent? Map(IDomainEvent @event)
    {
        return @event switch
        {

            _ => null
        };
    }

    public IEnumerable<IIntegrationEvent> MapAll(IEnumerable<IDomainEvent> events)
    {
        return events.Select(Map);
    }
}