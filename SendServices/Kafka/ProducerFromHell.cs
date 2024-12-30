using Confluent.Kafka;

namespace SendServices.Kafka;

public class ProducerFromHell
{
    private ProducerConfig _config;

    public ProducerFromHell()
    {
        _config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092"
        };
    }

    public async Task<DeliveryResult<Null, string>> Produce()
    {
        // Create a producer
        using (var producer = new ProducerBuilder<Null, string>(_config).Build())
        {
            var topic = "notifications-topic";

            // Produce a message
            var message = new Message<Null, string> { Value = "Hello, Kafka!" };
            return await producer.ProduceAsync(topic, message);
        }
    }
}