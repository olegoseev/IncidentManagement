using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Application.Responses;

namespace IoT.IncidentManagement.Application.Features.Notes.Commands.Create
{
    public class CreateNoteResponse : BaseResponse
    {
        public CreateNoteResponse() : base() { }
        public CreateNoteResponse(string message) : base(message) { }
        public CreateNoteResponse(string message, bool success) : base(message, success) { }

        public NoteDto Note { get; set; }
    }
}
