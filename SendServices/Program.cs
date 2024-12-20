using SendServices;

NotificationGateway gateway = new NotificationGateway();
Task<string> result = gateway.SendNotification();
Console.WriteLine("Wait result");
await result;
Console.WriteLine("Result: " + result.Result);