using IoT.IncidentManagement.Application.Models;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Severities.Commands.Create
{
    public class CreateSeverityRequest : IRequest<SeverityDto>
    {
        public string IncidentSeverity { get; set; }
        public int NotificationInterval { get; set; }
    }
}
