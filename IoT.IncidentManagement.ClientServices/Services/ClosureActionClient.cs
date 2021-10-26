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
    }
}
