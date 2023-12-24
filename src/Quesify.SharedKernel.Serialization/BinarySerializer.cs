using System.Runtime.Serialization;

namespace Quesify.SharedKernel.Utilities.Serializers.BinarySerializers;

public class BinarySerializer : IBinarySerializer
{
    public byte[] Serialize<T>(T value)
    {
        using var ms = new MemoryStream();
        var serializer = new DataContractSerializer(typeof(T));
        serializer.WriteObject(ms, value);
        return ms.ToArray();
    }

    public T? Deserialize<T>(byte[] value)
    {
        using var memStream = new MemoryStream(value);
        var serializer = new DataContractSerializer(typeof(T));
        var obj = (T?)serializer.ReadObject(memStream);
        return obj;
    }
}
