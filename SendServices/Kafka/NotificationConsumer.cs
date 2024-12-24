using Confluent.Kafka;

namespace SendServices.Kafka;

public class NotificationConsumer<T>
{
    private readonly string? _host;
    private readonly int _port;
    private readonly string? _topic;

    private NotificationGateway _notificationGateway;
    
    public NotificationConsumer()
    {
        _host = "localhost";
        _port = 9092;
        _topic = "producer_logs";
        _notificationGateway = new NotificationGateway();
    }

    ConsumerConfig GetConsumerConfig()
    {
        return new ConsumerConfig
        {
            BootstrapServers = $"{_host}:{_port}",
            GroupId = "foo",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
    }

    public async Task ConsumeAsync()
    {
        using (var consumer = new ConsumerBuilder<Ignore, T>(GetConsumerConfig())
                   .SetValueDeserializer(new NotificationSerialization<T>())
                   .Build())
        {
            consumer.Subscribe(_topic);

            Console.WriteLine($"Subscribed to {_topic}");

            await Task.Run(() =>
            {
                while (true)
                {
                    var consumeResult = consumer.Consume();

                    if (consumeResult.Message.Value is { } result)
                    {
                        Console.WriteLine($"Data Received - {result}");
                    }
                    else
                    {
                        Console.WriteLine("Received data is not Message or Null?");
                    }
                }
            });

            consumer.Close();
        }
    }
}