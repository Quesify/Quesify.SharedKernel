using Microsoft.Extensions.DependencyInjection;
using Quesify.SharedKernel.Storage.Local;
using Quesify.SharedKernel.Utilities.Guards;

namespace Quesify.SharedKernel.Storage;

public static class StorageOptionsExtensions
{
    public static StorageOptions UseLocalStorage(
        this StorageOptions storageOptions,
        IServiceCollection services)
    {
        Guard.Against.Null(storageOptions, nameof(storageOptions));
        Guard.Against.Null(services, nameof(services));

        services.AddSingleton<IStorageService, LocalStorageService>();
        return storageOptions;
    }
}
