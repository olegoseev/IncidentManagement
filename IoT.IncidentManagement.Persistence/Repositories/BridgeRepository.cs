using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Domain.Entities;
using IoT.IncidentManagement.Persistence.Context;

using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

namespace IoT.IncidentManagement.Persistence.Repositories
{
    public class BridgeRepository : AppRepository<Bridge>, IBridgeRepository
    {
        public BridgeRepository(IncidentManagementDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> BridgeTypeExist(string bridgeType) => dbContext.Set<Bridge>().AnyAsync(x => x.BridgeType == bridgeType);

        public Task<Bridge> GetByBridgeType(string bridgeType) =>
            dbContext.Set<Bridge>().SingleOrDefaultAsync(b => b.BridgeType == bridgeType);

    }
}
