using IoT.IncidentManagement.Application.Models;

using MediatR;

namespace IoT.IncidentManagement.Application.Features.Participants.Commands.Get.Details
{
    public class GetParticipantDetailsRequest : IRequest<ParticipantDto>
    {
        public int Id { get; set; }
    }
}
