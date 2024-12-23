using Newtonsoft.Json;

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
    private readonly ISendingChannel[] _sendingChannels =
    {
        new EmailChannel()
    };

    public async Task<string> SendNotification()
    {
        using StreamReader sr =
            new StreamReader("C:\\Users\\pathf\\RiderProjects\\SendServices\\SendServices\\testRequest.json");
        var json = sr.ReadToEnd();
        var request = JsonConvert.DeserializeObject<RequestItem>(json);

        foreach (var sendingChannel in _sendingChannels)
        {
            if (!sendingChannel.CanSend(request.Type))
                continue;

            var result = await Task.Run(() => sendingChannel.Send(request.Address, request.Content));
            return result;
        }

        return "Failure";
    }
}