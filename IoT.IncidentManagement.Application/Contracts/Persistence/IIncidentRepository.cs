using IoT.IncidentManagement.Domain.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Contracts.Persistence
{
    public interface IIncidentRepository : IAppRepository<Incident>
    {
        public Task<Incident> GetIncidentDetailsByIdAsync(int id, bool extended = false);
        public Task<IEnumerable<Incident>> GetAllWithDetailsAsync(bool getAllIncidents, string incidentStatus);
        public Task<Incident> GetByIncidentCaseAsync(string incidentCase);
        public Task<bool> IncidentWithCaseExistAsync(string incidentCase);
        public Task<bool> IncidentExistAsync(int id);
    }
}
