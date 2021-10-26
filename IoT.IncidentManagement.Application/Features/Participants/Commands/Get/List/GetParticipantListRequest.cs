using IoT.IncidentManagement.Application.Models;

using MediatR;

using System.Collections.Generic;

namespace IoT.IncidentManagement.Application.Features.Participants.Commands.Get.List
{
    public class GetParticipantListRequest : IRequest<IReadOnlyList<ParticipantDto>>
    {

    }
}
