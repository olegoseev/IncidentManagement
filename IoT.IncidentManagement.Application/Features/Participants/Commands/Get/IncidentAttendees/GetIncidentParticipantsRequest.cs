using IoT.IncidentManagement.Application.Models;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Participants.Commands.Get.IncidentAttendees
{
    public class GetIncidentParticipantsRequest : IRequest<ParticipantDto>
    {
        public int IncidentId { get; set; }
    }
}
