namespace Quesify.SharedKernel.EventBus.Kafka;

public interface IKafkaSerilazer
{
    byte[] Serilaze(object value);

    object? Deserilaze(byte[] value, Type type);
}
