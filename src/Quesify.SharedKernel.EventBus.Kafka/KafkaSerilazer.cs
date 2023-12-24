using Quesify.SharedKernel.Json;
using System.Text;

namespace Quesify.SharedKernel.EventBus.Kafka;

public class KafkaSerilazer : IKafkaSerilazer
{
    private readonly IJsonSerializer _jsonSerilazer;

    public KafkaSerilazer(IJsonSerializer jsonSerilazer)
    {
        _jsonSerilazer = jsonSerilazer;
    }

    public byte[] Serilaze(object value)
    {
        var jsonValue = _jsonSerilazer.Serialize(value);
        return Encoding.UTF8.GetBytes(jsonValue);
    }

    public object? Deserilaze(byte[] value, Type type)
    {
        var jsonValue = Encoding.UTF8.GetString(value);
        return _jsonSerilazer.Deserialize(jsonValue, type);
    }
}
