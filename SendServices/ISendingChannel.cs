namespace SendServices;

public interface ISendingChannel
{
    public bool CanSend(string type);
    public string Send(string address, string message);
}