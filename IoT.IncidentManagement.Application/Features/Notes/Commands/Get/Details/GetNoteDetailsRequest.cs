using IoT.IncidentManagement.Application.Models;

using MediatR;

namespace IoT.IncidentManagement.Application.Features.Notes.Commands.Get.Details
{
    public class GetNoteDetailsRequest : IRequest<NoteDto>
    {
        public int Id { get; set; }
    }
}
