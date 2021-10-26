using IoT.IncidentManagement.Domain.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Contracts.Persistence
{
    public interface INoteRepository : IAppRepository<Note>
    {
        Task<IEnumerable<Note>> GetByIncidentIdAsync(int IncidentId);
    }
}
