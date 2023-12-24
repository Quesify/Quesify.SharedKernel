using Microsoft.Extensions.DependencyInjection;
using Quesify.SharedKernel.Caching.Redis;
using Quesify.SharedKernel.Utilities.Guards;
using StackExchange.Redis;

namespace Quesify.SharedKernel.Caching;

public static class CacheOptionsExtensions
{
    public static CacheOptions UseRedis(
        this CacheOptions cacheOptions,
        IServiceCollection services,
        Action<RedisCacheOptions> configureRedisCacheOptions)
    {
        Guard.Against.Null(cacheOptions, nameof(cacheOptions));
        Guard.Against.Null(services, nameof(services));
        Guard.Against.Null(configureRedisCacheOptions, nameof(configureRedisCacheOptions));

        RedisCacheOptions redisCacheOptions = new RedisCacheOptions();
        configureRedisCacheOptions(redisCacheOptions);

        services.AddSingleton<RedisCacheOptions>();

        var endPoints = new EndPointCollection
        {
            { redisCacheOptions.Host, redisCacheOptions.Port },
        };

        services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(new ConfigurationOptions()
        {
            EndPoints = endPoints
        }));

        services.AddSingleton<ICacheService, RedisCacheService>();

        return cacheOptions;
    }
}
