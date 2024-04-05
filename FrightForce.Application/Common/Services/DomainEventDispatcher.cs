using FrightForce.Domain.Base;
using MediatR;

namespace FrightForce.Application.Common.Services;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IPublisher _publisher;

    public DomainEventDispatcher(IPublisher publisher)
    {
        _publisher = publisher;
    }


    public async Task Dispatch(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        await _publisher.Publish(domainEvent, cancellationToken);
    }

    public async Task Dispatch(IEnumerable<IDomainEvent> domainEvent, CancellationToken cancellationToken = default)
    {
        foreach (var @event in domainEvent)
        {
            await _publisher.Publish(@event, cancellationToken);
        }
    }
}