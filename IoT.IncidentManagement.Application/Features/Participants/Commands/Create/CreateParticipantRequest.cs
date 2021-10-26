using IoT.IncidentManagement.Application.Models;

using MediatR;

namespace IoT.IncidentManagement.Application.Features.Participants.Commands.Create
{
    public class CreateParticipantRequest : IRequest<ParticipantDto>
    {
        public string Group { get; set; }
        public int IncidentId { get; set; }
    }
}
