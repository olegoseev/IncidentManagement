using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Domain.Entities;
using IoT.IncidentManagement.Persistence.Context;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Persistence.Repositories
{
    public class ManagerActionRepository : AppRepository<ManagerAction>, IManagerActionRepository
    {
        public ManagerActionRepository(IncidentManagementDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<ManagerAction>> GetAllAsync(int incidentId)
        {
            return await dbContext.Set<ManagerAction>().Where(a => a.IncidentId == incidentId).ToListAsync();
        }

        public Task<ManagerAction> GetManagerActionNumber(int incidentId, int orderNumber)
        {
            return dbContext.Set<ManagerAction>().FirstOrDefaultAsync(a => a.IncidentId == incidentId &&
                    a.Order == orderNumber);
        }
    }
}
