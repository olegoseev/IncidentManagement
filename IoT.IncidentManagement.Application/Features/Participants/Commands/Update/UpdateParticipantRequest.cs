using MediatR;

namespace IoT.IncidentManagement.Application.Features.Participants.Commands.Update
{
    public class UpdateParticipantRequest : IRequest
    {
        public string Group { get; set; }
        public int IncidentId { get; set; }
    }
}
