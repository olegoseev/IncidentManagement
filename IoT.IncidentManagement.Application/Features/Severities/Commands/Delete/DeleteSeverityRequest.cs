using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Severities.Commands.Delete
{
    public class DeleteSeverityRequest : IRequest
    {
        public int Id { get; set; }
    }
}
