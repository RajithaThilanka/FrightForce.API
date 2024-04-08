using DotNetCore.CAP;
using FrightForce.Domain.Base;
using FrightForce.Domain.Identity;
using FrightForce.Infractructure.Persistence;
using FrightForce.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FrightForce.Infractructure.CAP;

public sealed class BusPublisher : IBusPublisher
    {
        private readonly IEventMapper _eventMapper;
        private readonly ILogger<BusPublisher> _logger;
        private readonly ICapPublisher _capPublisher;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ICurrentUserService _currentUserService;
        private readonly FrightForceDbContext _frightForceDbContext;

        public BusPublisher(IServiceScopeFactory serviceScopeFactory,
            IEventMapper eventMapper,
            ILogger<BusPublisher> logger,
            ICapPublisher capPublisher,
            IHttpContextAccessor httpContextAccessor,
            ICurrentUserService currentUserService,
            FrightForceDbContext frightForceDbContext)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _eventMapper = eventMapper;
            _logger = logger;
            _capPublisher = capPublisher;
            _currentUserService = currentUserService;
            _frightForceDbContext = frightForceDbContext;
        }

        public async Task SendAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default) =>
            await SendAsync(new[] { domainEvent }, cancellationToken);

        public async Task SendAsync(IReadOnlyList<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
        {
            if (domainEvents is null) return;

            _logger.LogTrace("Processing integration events start...");

            var integrationEvents = await MapDomainEventToIntegrationEventAsync(domainEvents).ConfigureAwait(false);

            if (!integrationEvents.Any()) return;

            foreach (var integrationEvent in integrationEvents)
            {
                await _capPublisher.PublishAsync(integrationEvent.GetType().Name, integrationEvent,
                    cancellationToken: cancellationToken);

                _logger.LogTrace("Publish a message with ID: {Id}", integrationEvent?.EventId);
            }

            _logger.LogTrace("Processing integration events done...");
        }


        public async Task SendAsync(IIntegrationEvent integrationEvent, CancellationToken cancellationToken = default) =>
            await SendAsync(new[] { integrationEvent }, cancellationToken);

        public async Task SendAsync(IReadOnlyList<IIntegrationEvent> integrationEvents,
            CancellationToken cancellationToken = default)
        {
            if (integrationEvents is null) return;

            _logger.LogTrace("Processing integration events start...");

            foreach (var integrationEvent in integrationEvents)
            {
                await _capPublisher.PublishAsync(integrationEvent.GetType().Name, integrationEvent,
                                                new Dictionary<string, string>()
                                                {
                                                     {"CompanyId", _frightForceDbContext.CompanyId.ToString()}
                                                },
                                                cancellationToken: cancellationToken);

                _logger.LogTrace("Publish a message with ID: {Id}", integrationEvent?.EventId);
            }

            _logger.LogTrace("Processing integration events done...");
        }

        private Task<IReadOnlyList<IIntegrationEvent>> MapDomainEventToIntegrationEventAsync(
            IReadOnlyList<IDomainEvent> events)
        {
            var wrappedIntegrationEvents = GetWrappedIntegrationEvents(events.ToList())?.ToList();
            if (wrappedIntegrationEvents?.Count > 0)
                return Task.FromResult<IReadOnlyList<IIntegrationEvent>>(wrappedIntegrationEvents);

            var integrationEvents = new List<IIntegrationEvent>();
            using var scope = _serviceScopeFactory.CreateScope();
            foreach (var @event in events)
            {
                var eventType = @event.GetType();
                _logger.LogTrace($"Handling domain event: {eventType.Name}");

                var integrationEvent = _eventMapper.Map(@event);

                if (integrationEvent is null) continue;

                integrationEvents.Add(integrationEvent);
            }

            return Task.FromResult<IReadOnlyList<IIntegrationEvent>>(integrationEvents);
        }

        private IEnumerable<IIntegrationEvent> GetWrappedIntegrationEvents(IReadOnlyList<IDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents.Where(x =>
                         x is IEmitsIntegrationEvent))
            {
                var genericType = typeof(IntegrationEventWrapper<>)
                    .MakeGenericType(domainEvent.GetType());

                var domainNotificationEvent = (IIntegrationEvent)Activator
                    .CreateInstance(genericType, domainEvent);

                yield return domainNotificationEvent;
            }
        }
    }