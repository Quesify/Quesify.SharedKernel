using Quesify.SharedKernel.Json;
using Quesify.SharedKernel.Utilities.Guards;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddJsonSerializer(
        this IServiceCollection services,
        Action<JsonSerializerOptions> configureJsonSerilazerOptions)
    {
        Guard.Against.Null(services, nameof(services));
        Guard.Against.Null(configureJsonSerilazerOptions, nameof(services));

        JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
        configureJsonSerilazerOptions(jsonSerializerOptions);

        return services.AddSingleton(jsonSerializerOptions);
    }
}

