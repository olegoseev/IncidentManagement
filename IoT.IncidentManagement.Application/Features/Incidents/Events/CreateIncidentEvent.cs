using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Incidents.Events
{
    public class CreateIncidentEvent : INotification
    {
        public Incident Incident { get; set; }
    }
}
