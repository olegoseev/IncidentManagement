using IoT.IncidentManagement.ClientApp.Models;
using IoT.IncidentManagement.ClientDomain.Entities;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Contracts
{
    public interface IManagerActionClient
    {
        public Task AddGroupAsync(ManagerActionDto body, CancellationToken cancellationToken);
        public Task<IEnumerable<ManagerAction>> GetManagerActionsAsync(ManagerActionDto dto, CancellationToken cancellationToken);
    }
}
