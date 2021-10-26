using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientApp.Models;
using IoT.IncidentManagement.ClientDomain.Entities;

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientServices.Services
{
    public class ParticipantClient : AppClient, IParticipantClient
    {
        public ParticipantClient(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task CreateParticipantsAsync(ParticipantsDto body, CancellationToken cancellationToken)
        {
            URL = $"api/participant";
            return AddAsync(body, cancellationToken);
        }

        public async Task<Participant> GetIncidentParticipantsByIncidentIdAsync(int IncidentId, CancellationToken cancellationToken)
        {
            URL = $"api/participant/{IncidentId}";
            return await GetAsync<Participant>(cancellationToken);
        }

        public Task UpdateParticipantsAsync(ParticipantsDto body, CancellationToken cancellationToken)
        {
            URL = $"api/participant";
            return UpdateAsync(body, cancellationToken);
        }
    }
}
