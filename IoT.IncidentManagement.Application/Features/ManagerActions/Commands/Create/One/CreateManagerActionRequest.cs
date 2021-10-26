using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;
using IoT.IncidentManagement.Domain.Enums;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.ManagerActions.Commands.Create.One
{
    public class CreateManagerActionRequest : IRequest<ManagerActionDto>
    {
        public int IncidentId { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public int Interval { get; set; }
        public bool Repeat { get; set; }
        public DateTime InitTime { get; set; }
        public NotificationState? State { get; set; }
    }
}
