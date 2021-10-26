using IoT.IncidentManagement.ClientApp.Models;
using IoT.IncidentManagement.ClientDomain.Entities;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Contracts
{
    public interface INoteClient
    {
        public Task<IEnumerable<Note>> GetIncidentNotesByIncidentIdAsync(int IncidentId, CancellationToken cancellationToken);
        Task<Note> AddNoteAsync(NoteDto body, CancellationToken cancellationToken);
    }
}
