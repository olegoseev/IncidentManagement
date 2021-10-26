using IoT.IncidentManagement.Domain.Entities;

using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Contracts.Persistence
{
    public interface IParticipantRepository : IAppRepository<Participant>
    {
        Task<Participant> GetByIncidentIdAsync(int incidentId);
    }
}
