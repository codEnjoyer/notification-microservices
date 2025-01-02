using System.Text;
using System.Text.Json;
using Confluent.Kafka;

namespace SendServices.Kafka;

public class KafkaProducer
{
    private readonly ProducerConfig _config;
    private readonly IProducer<string, string> _producer;

    public KafkaProducer()
    {
        _config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092"
        };
        _producer = new ProducerBuilder<string, string>(_config).Build();
    }

    public async Task TestProduce(string key, string value)
    {
        var topic = "notifications-topic";

        // Produce a message
        var message = new Message<string, string>
            { Key = key, Value = JsonSerializer.Serialize(value, typeof(string)) };
        await _producer.ProduceAsync(topic, message);
    }

    public async Task ProduceSendResponse(string result)
    {
        var topic = "notifications-topic";

        // Produce a message
        var message = new Message<string, string>
            { Key = "send_result", Value = JsonSerializer.Serialize(result, typeof(string)) };
        await _producer.ProduceAsync(topic, message);
    }
}