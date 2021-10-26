
using IoT.IncidentManagement.Application.Models;

using MediatR;

using System;

namespace IoT.IncidentManagement.Application.Features.Incidents.Commands.Create
{
    public class CreateIncidentRequest : IRequest<IncidentDto>
    {
        public string IncidentCase { get; set; }
        public string Description { get; set; }
        public string CustomerImpact { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime NotifiedTime { get; set; }
        public int SeverityId { get; set; }
        public int StatusId { get; set; }
        public int BridgeId { get; set; }
    }
}
