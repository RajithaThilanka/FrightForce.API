namespace FrightForce.Application.Features;

public interface ICacheableQuery
{
    string CacheKey { get; }
    TimeSpan? CacheExpiry { get; }
}