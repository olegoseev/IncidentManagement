using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Domain.Entities;
using IoT.IncidentManagement.Persistence.Context;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Persistence.Repositories
{
    public class NoteRepository : AppRepository<Note>, INoteRepository
    {
        public NoteRepository(IncidentManagementDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Note>> GetByIncidentIdAsync(int IncidentId)
        {
            return await dbContext.Set<Note>().Where(x => x.IncidentId == IncidentId).ToListAsync();
        }
    } 
}
