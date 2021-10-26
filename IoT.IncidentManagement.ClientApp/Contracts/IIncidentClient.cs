using IoT.IncidentManagement.ClientApp.Models;
using IoT.IncidentManagement.ClientDomain.Entities;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Contracts
{
    public interface IIncidentClient
    {
        public Task<Incident> GetIncidentByIdAsync(int IncidentId, CancellationToken cancellationToken);
        public Task<Incident> AddIncidentAsync(IncidentDto body, CancellationToken cancellationToken);
        public Task<IEnumerable<Incident>> GetAllIncidentsAsync(CancellationToken cancellationToken);
        public Task UpdateIncidentAsync(IncidentDto body, CancellationToken cancellationToken);
        public Task DeleteIncidentAsync(int IncidentId, CancellationToken cancellationToken);
    }
}
