using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Domain.Entities;
using IoT.IncidentManagement.Persistence.Context;

using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

namespace IoT.IncidentManagement.Persistence.Repositories
{
    public class ClosureActionRepository : AppRepository<ClosureAction>, IClosureActionRepository
    {
        public ClosureActionRepository(IncidentManagementDbContext dbContext) : base(dbContext)
        {
        }
    }
}
