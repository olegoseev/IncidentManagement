using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientDomain.Entities;

using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientServices.Services
{
    public class StatusClient : AppClient, IStatusClient
    {
        public StatusClient(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task<IEnumerable<Status>> GetAllStatusesAsync(CancellationToken cancellationToken)
        {
            URL = "api/Status/all";

            return GetAsync<IEnumerable<Status>>(cancellationToken);
        }
    }
}
