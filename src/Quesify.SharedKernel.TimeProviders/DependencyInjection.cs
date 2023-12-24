using Quesify.SharedKernel.TimeProviders;
using Quesify.SharedKernel.Utilities.Guards;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddTimeProvider(
        this IServiceCollection services,
        Action<TimeProviderOptions> configureTimeProviderOptions)
    {
        Guard.Against.Null(services, nameof(services));

        TimeProviderOptions timeProviderOptions = new TimeProviderOptions();
        configureTimeProviderOptions(timeProviderOptions);

        return services;
    }
}
