using SendServices;
using SendServices.Kafka;

var kafkaProducer = new KafkaProducer();
var gateway = new NotificationGateway(kafkaProducer);
var notificationConsumer = new NotificationConsumer(gateway);
Console.WriteLine("Start Listen messages");

// using StreamReader sr =
//     new StreamReader("C:\\Users\\pathf\\RiderProjects\\SendServices\\SendServices\\testRequest.json");
// var json = sr.ReadToEnd();
// kafkaProducer.TestProduce("send_request", json);

await notificationConsumer.ConsumeAsync();

Console.WriteLine("Notifications end work");