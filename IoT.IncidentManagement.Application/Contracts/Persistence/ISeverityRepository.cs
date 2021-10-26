
using System.Threading.Tasks;

using Severity = IoT.IncidentManagement.Domain.Entities.Severity;

namespace IoT.IncidentManagement.Application.Contracts.Persistence
{
    public interface ISeverityRepository : IAppRepository<Severity>
    {
        Task<bool> SeverityExist(string incidentSeverity);
        Task<Severity> GetBySeverity(string severity);
    }
}
