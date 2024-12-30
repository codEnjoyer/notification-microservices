using SendServices.Kafka;

var notificationConsumer = new NotificationConsumer<string>();
Console.WriteLine("Start Listen messages");

var producer = new ProducerFromHell();
var result = await producer.Produce();

await notificationConsumer.ConsumeAsync();

Console.WriteLine("Notifications end work");