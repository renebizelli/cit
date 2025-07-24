using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Ambev.DeveloperEvaluation.Cache.Redis;

public static class RedisConfiguration
{
    public static void AddRedis(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IConnectionMultiplexer>(s =>
        {
            var connectionString = builder.Configuration.GetConnectionString("Redis");
            ArgumentNullException.ThrowIfNull(connectionString);

            var configuration = ConfigurationOptions.Parse(connectionString);
            configuration.AbortOnConnectFail = false;
            return ConnectionMultiplexer.Connect(connectionString);
        });
    }
}
