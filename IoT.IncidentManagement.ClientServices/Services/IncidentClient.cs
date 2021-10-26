using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientApp.Models;
using IoT.IncidentManagement.ClientDomain.Entities;

using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientServices.Services
{
    public class IncidentClient : AppClient, IIncidentClient
    {
        public IncidentClient(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task<Incident> GetIncidentByIdAsync(int IncidentId, CancellationToken cancellationToken)
        {
            URL = $"api/incident/{IncidentId}";
            return GetAsync<Incident>(cancellationToken);
        }

        public Task<Incident> AddIncidentAsync(IncidentDto body, CancellationToken cancellationToken)
        {
            URL = "api/incident";
            return AddAsync<IncidentDto, Incident>(body, cancellationToken);
        }

        public async Task UpdateIncidentAsync(IncidentDto body, CancellationToken cancellationToken)
        {
            URL = "api/incident";
            await UpdateAsync(body, cancellationToken);
        }

        public Task<IEnumerable<Incident>> GetAllIncidentsAsync(CancellationToken cancellationToken)
        {
            URL = "api/incident";
            return GetAsync<IEnumerable<Incident>>(cancellationToken);
        }

        public Task DeleteIncidentAsync(int IncidentId, CancellationToken cancellationToken)
        {
            URL = $"api/incident/{IncidentId}";
            return DeleteAsync(cancellationToken);
        }
    }
}
