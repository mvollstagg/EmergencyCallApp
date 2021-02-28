using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EmergencyCall.Services.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMessage(string messageSubject, string messageBody, string toMailAddress)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(_configuration["FromEmailAddress"], _configuration["FromEmailAddressPassword"]);
            MailMessage message = new MailMessage(_configuration["FromEmailAddress"], toMailAddress);
            message.IsBodyHtml = true;
            message.Subject = messageSubject;
            message.Body = messageBody;
            await client.SendMailAsync(message);
        }
    }
}
