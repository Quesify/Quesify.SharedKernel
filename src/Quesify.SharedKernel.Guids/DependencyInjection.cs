using Quesify.SharedKernel.Guids;
using Quesify.SharedKernel.Utilities.Guards;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddGuid(
        this IServiceCollection services,
        Action<GuidOptions> configureGuidOptions)
    {
        Guard.Against.Null(services, nameof(services));

        GuidOptions guidOptions = new GuidOptions();
        configureGuidOptions(guidOptions);

        return services;
    }
}
