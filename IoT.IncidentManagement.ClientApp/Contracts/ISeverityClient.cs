using IoT.IncidentManagement.ClientDomain.Entities;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Contracts
{
    public interface ISeverityClient
    {
        Task<IEnumerable<Severity>> GetAllSeveritiesAsync(CancellationToken cancellationToken);
    }
}
