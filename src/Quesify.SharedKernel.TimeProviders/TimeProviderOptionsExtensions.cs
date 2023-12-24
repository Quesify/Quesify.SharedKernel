using Quesify.SharedKernel.Utilities.Guards;
using Quesify.SharedKernel.Utilities.TimeProviders;
using Microsoft.Extensions.DependencyInjection;

namespace Quesify.SharedKernel.TimeProviders;

public static class TimeProviderOptionsExtensions
{
    public static TimeProviderOptions UseMachineDateTime(
        this TimeProviderOptions timeProviderOptions,
        IServiceCollection services)
    {
        Guard.Against.Null(timeProviderOptions, nameof(timeProviderOptions));
        Guard.Against.Null(services, nameof(services));

        services.AddSingleton<IDateTime, MachineDateTime>();
        return timeProviderOptions;
    }

    public static TimeProviderOptions UseUtcDateTime(
        this TimeProviderOptions timeProviderOptions,
        IServiceCollection services)
    {
        Guard.Against.Null(timeProviderOptions, nameof(timeProviderOptions));
        Guard.Against.Null(services, nameof(services));

        services.AddSingleton<IDateTime, UtcDateTime>();
        return timeProviderOptions;
    }
}
