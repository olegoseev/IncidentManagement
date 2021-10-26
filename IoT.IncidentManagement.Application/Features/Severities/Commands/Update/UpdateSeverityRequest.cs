using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Severities.Commands.Update
{
    public class UpdateSeverityRequest : IRequest
    {
        public int Id { get; set; }
        public string IncidentSeverity { get; set; }
        public int NotificationInterval { get; set; }
    }
}
