using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientApp.Models;
using IoT.IncidentManagement.ClientDomain.Entities;

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientServices.Services
{
    public class ClosureActionClient : AppClient, IClosureActionClient
    {
        public ClosureActionClient(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task<ClosureAction> AddClosureActionAsync(ClosureActionDto dto, CancellationToken cancellationToken)
        {
            URL = "api/ClosureAction";
            return AddAsync<ClosureActionDto, ClosureAction>(dto, cancellationToken);
        }

        public Task<bool> ClosureActionsExistAsync(int incidentId, CancellationToken cancellationToken)
        {
            URL = $"api/ClosureAction/{incidentId}/status";
            return GetAsync<bool>(cancellationToken);
        }

        public Task<ClosureActionDto> GetClosureActionAsync(int incidentId, CancellationToken cancellationToken)
        {
            URL = $"api/ClosureAction/{incidentId}";
            return GetAsync<ClosureActionDto>(cancellationToken);
        }

        public Task UpdateClosureActionAsync(ClosureActionDto dto, CancellationToken cancellationToken)
        {
            URL = $"api/ClosureAction";
            return UpdateAsync(dto, cancellationToken);
        }
    }
}
