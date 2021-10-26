using IoT.IncidentManagement.Domain.Entities;

using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Contracts.Persistence
{
    public interface IBridgeRepository : IAppRepository<Bridge>
    {
        public Task<bool> BridgeTypeExist(string bridgeType);
        public Task<Bridge> GetByBridgeType(string bridgeType);
    }
}
