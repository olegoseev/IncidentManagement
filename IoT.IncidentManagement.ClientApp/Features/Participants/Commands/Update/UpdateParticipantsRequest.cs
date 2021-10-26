using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Participants.Commands.Update
{
    public class UpdateParticipantsRequest : IRequest
    {
        public int IncidentId { get; set; }
        public string Group { get; set; }
    }
}
