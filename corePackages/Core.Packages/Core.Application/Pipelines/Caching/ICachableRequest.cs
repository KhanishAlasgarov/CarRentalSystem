using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Core.Application.Pipelines.Caching;

public interface ICachableRequest
{
    string CacheKey { get; }
    bool BypassCache { get; }
    TimeSpan? SlidingExpiration { get; }
}

public class ChachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ICachableRequest
{
    public ChachingBehavior(CacheSettings cacheSettings, IDistributedCache distributedCache)
    {
        _cacheSettings = cacheSettings;
        _distributedCache = distributedCache;
    }

    private CacheSettings _cacheSettings { get; }
    private IDistributedCache _distributedCache { get; }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request.BypassCache)
        {
            return await next();
        }
        TResponse? response;
        byte[]? cacheResponse = await _distributedCache.GetAsync(request.CacheKey, cancellationToken);
        if (cacheResponse != null)
        {
            response = JsonSerializer.Deserialize<TResponse>(Encoding.Default.GetString(cacheResponse));
        }
        else
        {
            response = await getResponseAndAddToCache(request, next, cancellationToken);
        }

        return response;
    }

    private async Task<TResponse?> getResponseAndAddToCache(TRequest request,
        RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse response = await next();
        TimeSpan slidingExpiration = request.SlidingExpiration ?? TimeSpan.FromDays(_cacheSettings.SlidingExpiration);

        DistributedCacheEntryOptions cacheOptions = new() { SlidingExpiration = slidingExpiration };

        byte[] serializedData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response));
        await _distributedCache.SetAsync(request.CacheKey, serializedData, cacheOptions, cancellationToken);
        return response;
    }
}