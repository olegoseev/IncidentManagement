using IoT.IncidentManagement.ClientDomain.Entities;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Participants.Commands.Get
{
    public class GetParticipantsRequest : IRequest<Participant>
    {
        public int IncidentId { get; set; }
    }
}
