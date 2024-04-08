using FrightForce.Application.Base;
using FrightForce.Application.Features;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace FrightForce.Application.Common.Cashing;

public sealed class QueryCachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICacheableQuery
    where TResponse : Result
{
    private readonly IMemoryCache _cache;
    private readonly ILogger<QueryCachingBehavior<TRequest, TResponse>> _logger;

    public QueryCachingBehavior(IMemoryCache cache, ILogger<QueryCachingBehavior<TRequest, TResponse>> logger)
    {
        _cache = cache;
        _logger = logger;
    }
    

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse cachedResponse;
        if (_cache.TryGetValue(request.CacheKey, out cachedResponse))
        {
            _logger.LogInformation($"Cache hit for {typeof(TRequest).Name}");
            return cachedResponse;
        }

        _logger.LogInformation($"Cache miss for {typeof(TRequest).Name}");
        var response = await next();

        if (response.Success)
        {
            _cache.Set(request.CacheKey, response, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = request.CacheExpiry
            });
        }

        return response;
    }
}