using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.NotificationStateService.Features.StateMachines.Delete
{
    public class DeleteStateMachineRequest : IRequest
    {
        public string MachineName { get; set; }
    }
}
