namespace Quesify.SharedKernel.Utilities.Serializers.BinarySerializers;

public interface IBinarySerializer
{
    byte[] Serialize<T>(T value);

    T? Deserialize<T>(byte[] value);
}
