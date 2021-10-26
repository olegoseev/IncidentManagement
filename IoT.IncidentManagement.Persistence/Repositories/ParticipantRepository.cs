using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Domain.Entities;
using IoT.IncidentManagement.Persistence.Context;

using Microsoft.EntityFrameworkCore;

using System.Linq;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Persistence.Repositories
{
    public class ParticipantRepository : AppRepository<Participant>, IParticipantRepository
    {
        public ParticipantRepository(IncidentManagementDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Participant> GetByIncidentIdAsync(int incidentId) =>
            dbContext.Set<Participant>().SingleOrDefaultAsync(p => p.IncidentId == incidentId);

    }
}
