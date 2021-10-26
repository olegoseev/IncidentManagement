using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Application.Responses;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Severities.Commands.Create
{
    public class CreateSeverityResponse : BaseResponse
    {
        public CreateSeverityResponse() : base() { }
        public CreateSeverityResponse(string message) : base(message) { }
        public CreateSeverityResponse(string message, bool success) : base(message, success) { }

        public SeverityDto Severity { get; set; }
    }
}
