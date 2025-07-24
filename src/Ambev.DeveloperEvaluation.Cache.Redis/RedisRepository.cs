using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using StackExchange.Redis;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.Cache.Redis;

public class RedisRepository : ICacheRepository
{
    private readonly IDatabase cache;
    public RedisRepository(IConnectionMultiplexer multiplexer)
    {
        cache = multiplexer.GetDatabase();
    }

    public async Task SetAsync<T>(CacheSetOptions cacheSetOptions, T data)
    {
        var json = JsonSerializer.Serialize(data);

        await cache.StringSetAsync(cacheSetOptions.Key, json, cacheSetOptions.Expiry);
    }

    public async Task<T?> GetAsync<T>(CacheGetOptions cacheGetOptions)
    {
        var json = await cache.StringGetAsync(cacheGetOptions.Key);

        if (json.IsNull) return default(T);

        return JsonSerializer.Deserialize<T>(json.ToString());
    }
}
