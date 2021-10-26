using MediatR;

namespace IoT.IncidentManagement.Application.Features.Notes.Commands.Delete
{
    public class DeleteNoteRequest : IRequest
    {
        public int Id { get; set; }

    }
}
