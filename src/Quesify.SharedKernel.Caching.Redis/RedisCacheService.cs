using Quesify.SharedKernel.Json;
using StackExchange.Redis;

namespace Quesify.SharedKernel.Caching.Redis;

public class RedisCacheService : IRedisCacheService
{
    private readonly IDatabase _database;
    private readonly IServer _server;
    private readonly IJsonSerializer _jsonSerilazer;

    public RedisCacheService(
        IConnectionMultiplexer connectionMultiplexer,
        IJsonSerializer jsonSerilazer,
        RedisCacheOptions redisCacheOptions)
    {
        var endpoints = connectionMultiplexer.GetEndPoints();
        _server = connectionMultiplexer.GetServer(endpoints.First());
        _database = connectionMultiplexer.GetDatabase(redisCacheOptions.Database);
        _jsonSerilazer = jsonSerilazer;
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var cachedData = await _database.StringGetAsync(key);
        if (cachedData.IsNullOrEmpty)
        {
            return default;
        }
        return _jsonSerilazer.Deserialize<T>(cachedData!);
    }

    public async Task SetAsync(string key, object data, CancellationToken cancellationToken = default)
    {
        var jsonData = _jsonSerilazer.Serialize(data!);
        await _database.StringSetAsync(key, jsonData);
    }

    public async Task SetAsync(string key, object data, TimeSpan expiration, CancellationToken cancellationToken = default)
    {
        var jsonData = _jsonSerilazer.Serialize(data!);
        await _database.StringSetAsync(key, jsonData, expiration);
    }

    public async Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> data, TimeSpan expiration, CancellationToken cancellationToken = default)
    {
        if (await AnyAsync(key, cancellationToken))
        {
            return await GetAsync<T>(key, cancellationToken);
        }

        var value = await data.Invoke();
        await SetAsync(key, value!, expiration, cancellationToken);
        return value;
    }

    public async Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> data, CancellationToken cancellationToken = default)
    {
        if (await AnyAsync(key, cancellationToken))
        {
            return await GetAsync<T>(key, cancellationToken);
        }

        var value = await data.Invoke();
        await SetAsync(key, value!, cancellationToken);
        return value;
    }

    public async Task<bool> AnyAsync(string key, CancellationToken cancellationToken = default)
    {
        return await _database.KeyExistsAsync(key);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await _database.KeyDeleteAsync(key);
    }

    public async Task RemoveByPatternAsync(string pattern, CancellationToken cancellationToken = default)
    {
        var keys = _server.Keys(pattern: $"*{pattern}*");
        foreach (var key in keys)
        {
            await _database.KeyDeleteAsync(key);
        }
    }

    public async Task ClearAsync(CancellationToken cancellationToken = default)
    {
        await _server.FlushAllDatabasesAsync();
    }
}
