using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace Core.Application.Pipelines.Caching;
public class ChachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ICachableRequest
{
    public ChachingBehavior(IConfiguration configuration, IDistributedCache distributedCache)
    {
        //_cacheSettings = configuration.GetSection("CacheSettings").Get<CacheSettings>() ??
        //    throw new InvalidOperationException();
        _cacheSettings = new CacheSettings() { SlidingExpiration=2};
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
        if (request.CacheGroupKey != null)
            await addCacheKeyToGroup(request, slidingExpiration, cancellationToken);
        return response;
    }
    private async Task addCacheKeyToGroup(TRequest request, TimeSpan slidingExpiration, CancellationToken cancellationToken)
    {
        byte[]? cacheGroupCache = await _distributedCache.GetAsync(key: request.CacheGroupKey!, cancellationToken);
        HashSet<string> cacheKeysInGroup;
        if (cacheGroupCache != null)
        {
            cacheKeysInGroup = JsonSerializer.Deserialize<HashSet<string>>(Encoding.Default.GetString(cacheGroupCache))!;
            if (!cacheKeysInGroup.Contains(request.CacheKey))
                cacheKeysInGroup.Add(request.CacheKey);
        }
        else
            cacheKeysInGroup = new HashSet<string>(new[] { request.CacheKey });
        byte[] newCacheGroupCache = JsonSerializer.SerializeToUtf8Bytes(cacheKeysInGroup);

        byte[]? cacheGroupCacheSlidingExpirationCache = await _distributedCache.GetAsync(
            key: $"{request.CacheGroupKey}SlidingExpiration",
            cancellationToken
        );
        int? cacheGroupCacheSlidingExpirationValue = null;
        if (cacheGroupCacheSlidingExpirationCache != null)
            cacheGroupCacheSlidingExpirationValue = Convert.ToInt32(Encoding.Default.GetString(cacheGroupCacheSlidingExpirationCache));
        if (cacheGroupCacheSlidingExpirationValue == null || slidingExpiration.TotalSeconds > cacheGroupCacheSlidingExpirationValue)
            cacheGroupCacheSlidingExpirationValue = Convert.ToInt32(slidingExpiration.TotalSeconds);
        byte[] serializeCachedGroupSlidingExpirationData = JsonSerializer.SerializeToUtf8Bytes(cacheGroupCacheSlidingExpirationValue);

        DistributedCacheEntryOptions cacheOptions =
            new() { SlidingExpiration = TimeSpan.FromSeconds(Convert.ToDouble(cacheGroupCacheSlidingExpirationValue)) };

        await _distributedCache.SetAsync(key: request.CacheGroupKey!, newCacheGroupCache, cacheOptions, cancellationToken);
 

        await _distributedCache.SetAsync(
            key: $"{request.CacheGroupKey}SlidingExpiration",
            serializeCachedGroupSlidingExpirationData,
            cacheOptions,
            cancellationToken
        ); 
    }
}