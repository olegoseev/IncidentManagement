using IoT.IncidentManagement.ClientApp.Models;
using IoT.IncidentManagement.ClientDomain.Entities;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Contracts
{
    public interface IParticipantClient
    {
        public Task<Participant> GetIncidentParticipantsByIncidentIdAsync(int IncidentId, CancellationToken cancellationToken);

        public Task UpdateParticipantsAsync(ParticipantsDto body, CancellationToken cancellationToken);
        public Task CreateParticipantsAsync(ParticipantsDto body, CancellationToken cancellationToken);
    }
}
