namespace Quesify.SharedKernel.Caching.Redis;

public class RedisCacheOptions
{
    public int Database { get; set; }

    public string Host { get; set; }

    public int Port { get; set; }

    public RedisCacheOptions()
    {
        Database = 0;
        Host = string.Empty;
    }
}
