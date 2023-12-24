namespace Quesify.SharedKernel.Caching;

public interface ICacheKeyGenerator
{
    string GenerateCacheKey(string name, params object[] values);
}
