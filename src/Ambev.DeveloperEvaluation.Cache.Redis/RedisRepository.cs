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

    public async Task SetAsync<T>(string key, T data)
    {
        var json = JsonSerializer.Serialize(data);

        await cache.StringSetAsync(key, json);
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var json = await cache.StringGetAsync(key);

        if (json.IsNull) return default(T);

        return JsonSerializer.Deserialize<T>(json.ToString());
    }
}
