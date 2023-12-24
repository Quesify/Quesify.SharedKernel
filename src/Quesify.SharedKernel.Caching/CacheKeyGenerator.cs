using Quesify.SharedKernel.Utilities.Serializers.BinarySerializers;

namespace Quesify.SharedKernel.Caching;

public class CacheKeyGenerator : ICacheKeyGenerator
{
    private readonly CacheOptions _cacheOptions;
    private readonly IBinarySerializer _binarySerializer;

    public CacheKeyGenerator(
        CacheOptions cacheOptions,
        IBinarySerializer binarySerializer)
    {
        _cacheOptions = cacheOptions;
        _binarySerializer = binarySerializer;
    }

    public string GenerateCacheKey(string name, params object[] values)
    {
        byte[] valueBytes = _binarySerializer.Serialize(values);
        var valueBase64 = Convert.ToBase64String(valueBytes);
        return $"{GetCacheKeyPrefix()}{name}{GetCacheKeySuffix()}({valueBase64})";
    }

    private string GetCacheKeyPrefix()
    {
        return _cacheOptions.KeyPrefix.IsNullOrEmpty() ? string.Empty : $"{_cacheOptions.KeyPrefix}.";
    }

    private string GetCacheKeySuffix()
    {
        return _cacheOptions.KeySuffix.IsNullOrEmpty() ? string.Empty : $".{_cacheOptions.KeySuffix}";
    }
}
