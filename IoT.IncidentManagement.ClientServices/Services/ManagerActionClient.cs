using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientApp.Models;
using IoT.IncidentManagement.ClientDomain.Entities;

using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientServices.Services
{
    public class ManagerActionClient : AppClient, IManagerActionClient
    {
        public ManagerActionClient(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task AddGroupAsync(ManagerActionDto body, CancellationToken cancellationToken)
        {
            URL = "api/ManagerAction/group";

            return AddAsync(body, cancellationToken);
        }

        public Task<IEnumerable<ManagerAction>> GetManagerActionsAsync(ManagerActionDto dto, CancellationToken cancellationToken)
        {
            URL = $"api/ManagerAction/{dto.IncidentId}/all";
            return GetAsync<IEnumerable<ManagerAction>>(cancellationToken);
        }
    }
}
