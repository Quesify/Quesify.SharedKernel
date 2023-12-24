using Quesify.SharedKernel.Storage;
using Quesify.SharedKernel.Utilities.Guards;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddStorage(
        this IServiceCollection services,
        Action<StorageOptions> configureStorageOptions)
    {
        Guard.Against.Null(services, nameof(services));

        StorageOptions storageOptions = new StorageOptions();
        configureStorageOptions(storageOptions);

        return services.AddSingleton(storageOptions);
    }
}
