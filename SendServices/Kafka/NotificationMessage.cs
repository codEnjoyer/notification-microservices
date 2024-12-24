namespace SendServices.Kafka;

public class NotificationMessage
{
    public string ID { get; set; } = Guid.NewGuid().ToString();
    public string CreatedAt { get; set; }
    public string UpdatedAt { get; set; }
    public string State { get; set; }
    public string Body { get; set; }
}