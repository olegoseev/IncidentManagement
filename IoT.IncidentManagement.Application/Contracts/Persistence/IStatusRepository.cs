using IoT.IncidentManagement.Domain.Entities;

using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Contracts.Persistence
{
    public interface IStatusRepository : IAppRepository<Status>
    {
        public Task<bool> StatusExistsAsync(string status);
        public Task<Status> GetStatusByCurrentStatus(string currentStatus);
    }
}
