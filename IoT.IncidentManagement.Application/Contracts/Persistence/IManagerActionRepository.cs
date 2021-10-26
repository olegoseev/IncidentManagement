using IoT.IncidentManagement.Domain.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Contracts.Persistence
{
    public interface IManagerActionRepository : IAppRepository<ManagerAction>
    {
        Task<ManagerAction> GetManagerActionNumber(int incidentId, int orderNumber);
        Task<IEnumerable<ManagerAction>> GetAllAsync(int incidentId);
    }
}
