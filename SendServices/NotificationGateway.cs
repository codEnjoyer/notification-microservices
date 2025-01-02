using Newtonsoft.Json;
using SendServices.Kafka;

namespace SendServices;

public struct RequestItem
{
    public string Type = "None";
    public string Address = "None";
    public string Content = "None";

    public RequestItem()
    {
    }
}

public class NotificationGateway
{
    private readonly KafkaProducer _kafkaProducer;
    private readonly ISendingChannel[] _sendingChannels =
    {
        new EmailChannel()
    };

    public NotificationGateway(KafkaProducer kafkaProducer)
    {
        _kafkaProducer = kafkaProducer;
    }

    public async Task SendNotification(string messageJson)
    {
        Console.WriteLine("Get message " + messageJson);
        var request = JsonConvert.DeserializeObject<RequestItem>(messageJson);

        foreach (var sendingChannel in _sendingChannels)
        {
            if (!sendingChannel.CanSend(request.Type))
                continue;

            var result = await Task.Run(() => sendingChannel.Send(request.Address, request.Content));

            await _kafkaProducer.ProduceSendResponse(result);
            return;
        }

        await _kafkaProducer.ProduceSendResponse("Failure");
    }
}