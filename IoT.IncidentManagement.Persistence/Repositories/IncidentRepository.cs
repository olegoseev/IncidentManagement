using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Domain.Entities;
using IoT.IncidentManagement.Persistence.Context;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Persistence.Repositories
{
    public class IncidentRepository : AppRepository<Incident>, IIncidentRepository
    {

        public IncidentRepository(IncidentManagementDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Incident>> GetAllWithDetailsAsync(bool getAllIncidents, string incidentStatus)
        {
            return await dbContext.Set<Incident>()
                            .Include(x => x.Bridge)
                            .Include(x => x.Severity)
                            .Include(x => x.Status)
                            .ToListAsync();
        }

        public Task<Incident> GetIncidentDetailsByIdAsync(int id, bool extended = false)
        {
           return extended ?
                 dbContext.Set<Incident>().Where(x => x.Id == id)
                            .Include(x => x.Bridge)
                            .Include(x => x.ClosureAction)
                            .Include(x => x.Notes)
                            .Include(x => x.Participant)
                            .Include(x => x.Severity)
                            .Include(x => x.Status)
                            .SingleOrDefaultAsync()
                            :
                dbContext.Set<Incident>().Where( x => x.Id == id)
                            .Include(x => x.Bridge)
                            .Include(x => x.Severity)
                            .Include(x => x.Status)
                            .SingleOrDefaultAsync();
        }

        public Task<Incident> GetByIncidentCaseAsync(string incidentCase) =>
                        dbContext.Set<Incident>().SingleOrDefaultAsync(x => x.IncidentCase == incidentCase);
        
        public Task<bool> IncidentWithCaseExistAsync(string incidentCase) => dbContext.Set<Incident>().AnyAsync(x => x.IncidentCase == incidentCase);


        public async Task<bool> IncidentExistAsync(int id)
        {
            var found = await dbContext.Set<Incident>().FindAsync(id);
            return found != null;
        }
    }
}
