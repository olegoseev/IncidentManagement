using IoT.IncidentManagement.ClientDomain.Entities;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Contracts
{
    public interface IStatusClient
    {
        Task<IEnumerable<Status>> GetAllStatusesAsync(CancellationToken cancellationToken);
    }
}
