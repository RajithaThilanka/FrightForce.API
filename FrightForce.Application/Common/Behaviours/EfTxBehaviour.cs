using System.Text.Json;
using FrightForce.Domain.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FrightForce.Application.Common.Behaviours;

public class EfTxBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
    {
        private readonly ILogger<EfTxBehaviour<TRequest, TResponse>> _logger;
        private readonly ITransactionContextAwareDbContext _dbContextBase;
        private readonly IPublisher _publisher;

        public EfTxBehaviour(
            ILogger<EfTxBehaviour<TRequest, TResponse>> logger,
            ITransactionContextAwareDbContext dbContextBase,
            IPublisher busPublisher
        )
        {
            _logger = logger;
            _dbContextBase = dbContextBase;
            _publisher = busPublisher;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "{Prefix} Handled command {MediatrRequest}",
                nameof(EfTxBehaviour<TRequest, TResponse>),
                typeof(TRequest).FullName);

            _logger.LogDebug(
                "{Prefix} Handled command {MediatrRequest} with content {RequestContent}",
                nameof(EfTxBehaviour<TRequest, TResponse>),
                typeof(TRequest).FullName,
                JsonSerializer.Serialize(request));

            _logger.LogInformation(
                "{Prefix} Open the transaction for {MediatrRequest}",
                nameof(EfTxBehaviour<TRequest, TResponse>),
                typeof(TRequest).FullName);

            await _dbContextBase.BeginTransactionAsync(cancellationToken);

            try
            {
                var response = await next();

                _logger.LogInformation(
                    "{Prefix} Executed the {MediatrRequest} request",
                    nameof(EfTxBehaviour<TRequest, TResponse>),
                    typeof(TRequest).FullName);

                var domainEvents = _dbContextBase.GetDomainEvents();

                await Publish(domainEvents.ToArray(), cancellationToken);

                await _dbContextBase.CommitTransactionAsync(cancellationToken);

                return response;
            }
            catch
            {
                await _dbContextBase.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        private async Task Publish(IDomainEvent[] domainEvents, CancellationToken cancellationToken)
        {
            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }
        }
    }