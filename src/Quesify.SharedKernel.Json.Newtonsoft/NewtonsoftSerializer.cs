using Newtonsoft.Json;

namespace Quesify.SharedKernel.Json.Newtonsoft;

public class NewtonsoftSerializer : IJsonSerializer
{
    private readonly JsonSerializerSettings _jsonSerializerSettings;

    public NewtonsoftSerializer(JsonSerializerSettings jsonSerializerSettings)
    {
        _jsonSerializerSettings = jsonSerializerSettings;
    }

    public string Serialize(object value)
    {
        return JsonConvert.SerializeObject(value, _jsonSerializerSettings);
    }

    public T? Deserialize<T>(string value)
    {
        return JsonConvert.DeserializeObject<T>(value, _jsonSerializerSettings);
    }

    public object? Deserialize(string value, Type type)
    {
        return JsonConvert.DeserializeObject(value, type, _jsonSerializerSettings);
    }
}
