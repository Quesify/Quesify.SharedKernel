using Confluent.Kafka;

namespace Quesify.SharedKernel.EventBus.Kafka;

public class KafkaOptions
{
    public ProducerConfig ProducerConfig { get; set; }

    public ConsumerConfig ConsumerConfig { get; set; }

    public AdminClientConfig AdminClientConfig { get; set; }

    public KafkaOptions()
    {
        ProducerConfig = new ProducerConfig();
        ConsumerConfig = new ConsumerConfig();
        AdminClientConfig = new AdminClientConfig();
    }
}
