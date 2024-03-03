using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace Core.Application.Pipelines.Caching;

public class ChacheRemovingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ICacheRemoverRequest
{
    public ChacheRemovingBehavior(IDistributedCache distributedCache)
    {

        _distributedCache = distributedCache;
    }
    private IDistributedCache _distributedCache { get; }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request.BypassCache)
        {
            return await next();
        }
        TResponse response = await next();

        if (request.CacheGroupKey != null)
        {
            byte[]? cachedGroup = await _distributedCache.GetAsync(request.CacheGroupKey, cancellationToken);
            if (cachedGroup != null)
            {
                HashSet<string> keysInGroup = JsonSerializer.Deserialize<HashSet<string>>(Encoding.Default.GetString(cachedGroup))!;
                foreach (string key in keysInGroup)
                {
                    await _distributedCache.RemoveAsync(key, cancellationToken);
                }

                await _distributedCache.RemoveAsync(request.CacheGroupKey, cancellationToken);
                await _distributedCache.RemoveAsync(key: $"{request.CacheGroupKey}SlidingExpiration", cancellationToken);
            }
        }

        if (request.CacheKey != null)
        {
            await _distributedCache.RemoveAsync(request.CacheKey, cancellationToken);
        }

        return response;

    }
}
