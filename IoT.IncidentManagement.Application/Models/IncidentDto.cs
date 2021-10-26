
using System;

namespace IoT.IncidentManagement.Application.Models
{
    public class IncidentDto
    {
        public int Id { get; set; }
        public string IncidentCase { get; set; }
        public string Description { get; set; }
        public string CustomerImpact { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime NotifiedTime { get; set; }
        public SeverityDto Severity { get; set; }
        public StatusDto Status { get; set; }
        public BridgeDto Bridge { get; set; }
    }
}
