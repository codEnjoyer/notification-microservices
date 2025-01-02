using Telegram.Bot;
using Telegram.Bot.Types;

namespace SendServices;

public class TelegramChannel : ISendingChannel
{
    private const string ApiToken = "7770806075:AAHvbaYNTvaixuSTVrXYoSv3MFQ_GyFKKn4";
    private readonly TelegramBotClient _telegramBotClient = new(ApiToken);
    private readonly HashSet<(Chat chat, Contact contact)> _chats = new();

    public bool CanSend(string type) => type.ToLower() == "telegram";

    public void Init()
    {
        _telegramBotClient.OnMessage += (message, type) =>
        {
            _chats.Add((message.Chat, message.Contact)!);

            return Task.CompletedTask;
        };
    }

    public string Send(string address, string message)
    {
        var destination =
            _chats.FirstOrDefault(id => id.chat.Username == address || id.contact.UserId.ToString() == address);
        if (destination == default)
        {
            return "Unknown user address.";
        }

        _telegramBotClient.SendMessage(destination.chat, message);
        return "OK";
    }
}