using System.Threading.Tasks;

namespace EmergencyCall.Services.Services
{
    public interface IEmailSender
    {
        public Task SendMessage(string messageSubject, string messageBody, string toMailAddress);
    }
}
