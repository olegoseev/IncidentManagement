using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientDomain.Entities;

using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientServices.Services
{
    public class BridgeClient : AppClient, IBridgeClient
    {
        public BridgeClient(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task<IEnumerable<Bridge>> GetAllBridgesAsync(CancellationToken cancellationToken)
        {
            URL = "api/Bridge/all";

            return GetAsync<IEnumerable<Bridge>>(cancellationToken);
        }
    }
}
