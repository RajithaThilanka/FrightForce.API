namespace FrightForce.Domain.Base;

public record IntegrationEventWrapper<TDomainEventType>(TDomainEventType DomainEvent) : IIntegrationEvent
    where TDomainEventType : IDomainEvent;