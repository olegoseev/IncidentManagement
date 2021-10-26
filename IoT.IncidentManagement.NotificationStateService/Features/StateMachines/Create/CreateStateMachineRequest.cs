using IoT.IncidentManagement.ClientDomain.Entities;
using IoT.IncidentManagement.ClientDomain.Enum;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.NotificationStateService.Features.StateMachines.Create
{
    public class CreateStateMachineRequest : IRequest
    {
        public Incident Incident { get; set; }
        public NotificationGroup Group { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}
