using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientDomain.Entities;

using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientServices.Services
{
    public class SeverityClient : AppClient, ISeverityClient
    {
        public SeverityClient(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task<IEnumerable<Severity>> GetAllSeveritiesAsync(CancellationToken cancellationToken)
        {
            URL = "api/Severity/all";

            return GetAsync<IEnumerable<Severity>>(cancellationToken);
        }
    }
}
