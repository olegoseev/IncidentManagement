using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Domain.Entities;
using IoT.IncidentManagement.Persistence.Context;

using Microsoft.EntityFrameworkCore;

using System.Linq;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Persistence.Repositories
{
    public class StatusRepository : AppRepository<Status>, IStatusRepository
    {
        public StatusRepository(IncidentManagementDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Status> GetStatusByCurrentStatus(string currentStatus) =>
            dbContext.Set<Status>().SingleOrDefaultAsync(s => s.CurrentStatus == currentStatus);


        public Task<bool> StatusExistsAsync(string status) => dbContext.Set<Status>().AnyAsync(x => x.CurrentStatus == status);

    }
}
