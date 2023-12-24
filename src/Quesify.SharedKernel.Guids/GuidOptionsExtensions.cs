using Quesify.SharedKernel.Utilities.Guards;
using Microsoft.Extensions.DependencyInjection;

namespace Quesify.SharedKernel.Guids;

public static class GuidOptionsExtensions
{
    public static GuidOptions UseCustomGuidGenerator(
        this GuidOptions guidOptions,
        IServiceCollection services)
    {
        Guard.Against.Null(guidOptions, nameof(guidOptions));
        Guard.Against.Null(services, nameof(services));

        services.AddSingleton<IGuidGenerator, CustomGuidGenerator>();
        return guidOptions;
    }

    public static GuidOptions UseSequentialGuidGenerator(
        this GuidOptions guidOptions,
        IServiceCollection services)
    {
        Guard.Against.Null(guidOptions, nameof(guidOptions));
        Guard.Against.Null(services, nameof(services));

        services.AddSingleton<IGuidGenerator, SequentialGuidGenerator>();
        return guidOptions;
    }
}
