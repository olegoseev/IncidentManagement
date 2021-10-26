using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientApp.Models;
using IoT.IncidentManagement.ClientDomain.Entities;

using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientServices.Services
{
    public class NoteClient : AppClient, INoteClient
    {
        public NoteClient(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task<Note> AddNoteAsync(NoteDto body, CancellationToken cancellationToken)
        {
            URL = $"api/note/";
            return AddAsync<NoteDto, Note>(body, cancellationToken);
        }

        public Task<IEnumerable<Note>> GetIncidentNotesByIncidentIdAsync(int IncidentId, CancellationToken cancellationToken)
        {
            URL = $"api/note/{IncidentId}/all";
            return GetAsync<IEnumerable<Note>>(cancellationToken);
        }
    }
}
