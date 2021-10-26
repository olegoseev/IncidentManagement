using IoT.IncidentManagement.ClientDomain.Entities;

using MediatR;

using System;

namespace IoT.IncidentManagement.ClientApp.Features.Incidents.Commands.Create
{
    public class CreateIncidentRequest : IRequest<Incident>
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
