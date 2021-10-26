using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Domain.Entities;
using IoT.IncidentManagement.Persistence.Context;

using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

namespace IoT.IncidentManagement.Persistence.Repositories
{
    public class SeverityRepository : AppRepository<Severity>, ISeverityRepository
    {
        public SeverityRepository(IncidentManagementDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Severity> GetBySeverity(string severity) =>
            dbContext.Set<Severity>().SingleOrDefaultAsync(s => s.IncidentSeverity == severity);

        public Task<bool> SeverityExist(string incidentSeverity) => dbContext.Set<Severity>().AnyAsync(s => s.IncidentSeverity == incidentSeverity);

    }
}
