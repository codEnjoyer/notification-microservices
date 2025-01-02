using Telegram.Bot;
using Telegram.Bot.Types;

namespace SendServices;

public class TelegramChannel : ISendingChannel
{
    private const string ApiToken = "7770806075:AAHvbaYNTvaixuSTVrXYoSv3MFQ_GyFKKn4";
    private readonly TelegramBotClient _telegramBotClient = new(ApiToken);
    private readonly HashSet<Chat> _chats = new();

    public bool CanSend(string type) => type.ToLower() == "telegram";

    public void Init()
    {
        _telegramBotClient.OnMessage += (message, type) =>
        {
            _chats.Add(message.Chat);
            
            return Task.CompletedTask;
        };
    }

    public string Send(string userName, string message)
    {
        var destinationChat = _chats.FirstOrDefault(id => id.Username == userName);
        if (destinationChat == default)
        {
            return "Unknown user address.";
        }

        _telegramBotClient.SendMessage(destinationChat, message);
        return "OK";
    }
}