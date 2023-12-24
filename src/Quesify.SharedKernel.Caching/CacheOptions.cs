namespace Quesify.SharedKernel.Caching;

public class CacheOptions
{
    public string KeyPrefix { get; set; }

    public string KeySuffix { get; set; }

    public CacheOptions()
    {
        KeyPrefix = string.Empty;
        KeySuffix = string.Empty;
    }
}
