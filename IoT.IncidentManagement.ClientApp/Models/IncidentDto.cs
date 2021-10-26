
using System;

namespace IoT.IncidentManagement.ClientApp.Models
{
    public class IncidentDto
    {
        public int Id { get; set; }
        public string IncidentCase { get; set; }
        public string Description { get; set; }
        public string CustomerImpact { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime NotifiedTime { get; set; }
        public DateTime EndTime { get; set; }
        public int SeverityId { get; set; }
        public int StatusId { get; set; }
        public int BridgeId { get; set; }
    }
}
