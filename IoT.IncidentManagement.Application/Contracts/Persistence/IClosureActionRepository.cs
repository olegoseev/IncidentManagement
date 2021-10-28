using IoT.IncidentManagement.Domain.Entities;

using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Contracts.Persistence
{
    public interface IClosureActionRepository : IAppRepository<ClosureAction>
    {
        public Task<bool> ClosureActionExists(int incidentId);
    }
}
