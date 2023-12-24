using Quesify.SharedKernel.Utilities.Guards;
using Quesify.SharedKernel.Utilities.Serializers.BinarySerializers;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddBinarySerializer(this IServiceCollection services)
    {
        Guard.Against.Null(services, nameof(services));
        return services.AddSingleton<IBinarySerializer, BinarySerializer>();
    }
}

