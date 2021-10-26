using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Participants.Commands.Delete
{
    public class DeleteParticipantRequest : IRequest
    {
        public int IncidentId { get; set; }
    }
}
