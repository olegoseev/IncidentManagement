using IoT.IncidentManagement.Application.Models;

using MediatR;

namespace IoT.IncidentManagement.Application.Features.Notes.Commands.Create
{
    public class CreateNoteRequest : IRequest<NoteDto>
    {
        public int IncidentId { get; set; }
        public string Record { get; set; }
    }
}
