using System.Net;
using System.Net.Mail;

namespace SendServices;

public class EmailChannel : ISendingChannel
{
    public bool CanSend(string type) => type.ToLower() == "email";

    public string Send(string address, string message)
    {
        MailAddress from = new MailAddress("mail.channel.one@gmail.com", "Email Notification");
        MailAddress to = new MailAddress(address, "SANO");

        SmtpClient smtpClient = new SmtpClient();

        smtpClient.Host = "smtp.gmail.com";
        smtpClient.Port = 587;
        smtpClient.EnableSsl = true;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential(from.Address, "aqgl uiwe vxev qvsj");

        MailMessage mailMessage = new MailMessage(from, to);
        mailMessage.Subject = "My Subject";
        mailMessage.Body = message;
        try
        {
            smtpClient.Send(mailMessage);
        }
        catch (Exception e)
        {
            return e.Message;
        }

        return "OK";
    }
}