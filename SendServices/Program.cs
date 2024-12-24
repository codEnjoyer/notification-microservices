using SendServices.Kafka;

var notificationConsumer = new NotificationConsumer<NotificationMessage>();
Console.WriteLine("Start Listen messages");  
await notificationConsumer.ConsumeAsync();
Console.WriteLine("Notifications end work");
