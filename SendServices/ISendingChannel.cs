namespace SendServices;

public interface ISendingChannel
{
    public bool CanSend(string type);

    public void Init();
    public string Send(string address, string message);
}