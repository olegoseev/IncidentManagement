using IoT.IncidentManagement.Application.Models.Mail;

using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
