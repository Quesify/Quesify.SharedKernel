namespace Quesify.SharedKernel.Caching;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);

    Task SetAsync(string key, object data, CancellationToken cancellationToken = default);

    Task SetAsync(string key, object data, TimeSpan expiration, CancellationToken cancellationToken = default);

    Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> data, TimeSpan expiration, CancellationToken cancellationToken = default);

    Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> data, CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(string key, CancellationToken cancellationToken = default);

    Task RemoveAsync(string key, CancellationToken cancellationToken = default);

    Task RemoveByPatternAsync(string pattern, CancellationToken cancellationToken = default);

    Task ClearAsync(CancellationToken cancellationToken = default);
}
