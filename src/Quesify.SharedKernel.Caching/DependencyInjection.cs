using Quesify.SharedKernel.Caching;
using Quesify.SharedKernel.Utilities.Guards;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddCaching(
        this IServiceCollection services,
        Action<CacheOptions> configureCacheOptions)
    {
        Guard.Against.Null(services, nameof(services));
        Guard.Against.Null(configureCacheOptions, nameof(configureCacheOptions));

        CacheOptions cacheOptions = new CacheOptions();
        configureCacheOptions(cacheOptions);

        return services
            .AddSingleton(cacheOptions)
            .AddSingleton<ICacheKeyGenerator, CacheKeyGenerator>();
    }
}
