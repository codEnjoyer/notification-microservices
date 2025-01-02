using Confluent.Kafka;

namespace SendServices.Kafka;

public class NotificationConsumer
{
    private readonly string? _host;
    private readonly int _port;
    private readonly string? _topic;

    private NotificationGateway _notificationGateway;

    public NotificationConsumer(NotificationGateway gateway)
    {
        _host = "localhost";
        _port = 9092;
        _topic = "notifications-topic";
        _notificationGateway = gateway;
    }

    ConsumerConfig GetConsumerConfig()
    {
        return new ConsumerConfig
        {
            BootstrapServers = $"{_host}:{_port}",
            GroupId = "my-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
    }

    public async Task ConsumeAsync()
    {
        using (var consumer = new ConsumerBuilder<string, string>(GetConsumerConfig())
                   .SetValueDeserializer(new NotificationSerialization<string>())
                   .Build())
        {
            consumer.Subscribe(_topic);

            Console.WriteLine($"Subscribed to {_topic}");

            await Task.Run(() =>
            {
                while (true)
                {
                    var consumeResult = consumer.Consume();

                    if (consumeResult != null && consumeResult.Message.Key == "send_request")
                    {
                        _notificationGateway.SendNotification(consumeResult.Message.Value);
                    }
                    else if (consumeResult != null)
                    {
                        Console.WriteLine("Test recieved - " + consumeResult.Message.Value);
                    }
                    else {
                        Console.WriteLine("Received data is not Message or Null?");
                    }
                }
            });

            consumer.Close();
        }
    }
}