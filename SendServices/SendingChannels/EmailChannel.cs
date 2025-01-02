using System.Net;
using System.Net.Mail;

namespace SendServices;

public class EmailChannel : ISendingChannel
{
    private MailAddress _originMailAddress;
    private SmtpClient _smtpClient;

    public bool CanSend(string type) => type.ToLower() == "email";

    public void Init()
    {
        _originMailAddress = new MailAddress("mail.channel.one@gmail.com", "Email Notification");
        _smtpClient = new SmtpClient();

        _smtpClient.Host = "smtp.gmail.com";
        _smtpClient.Port = 587;
        _smtpClient.EnableSsl = true;
        _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        _smtpClient.UseDefaultCredentials = false;
        _smtpClient.Credentials = new NetworkCredential(_originMailAddress.Address, "aqgl uiwe vxev qvsj");
    }

    public string Send(string address, string message)
    {
        MailAddress destinationAddress = new MailAddress(address, "SANO");

        MailMessage mailMessage = new MailMessage(_originMailAddress, destinationAddress);
        mailMessage.Subject = "My Subject";
        mailMessage.Body = message;
        try
        {
            _smtpClient.Send(mailMessage);
        }
        catch (Exception e)
        {
            return e.Message;
        }

        return "OK";
    }
}