
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientDomain.Entities
{
    public class Incident
    {
        public int Id { get; set; }
        public string IncidentCase { get; set; }
        public string Description { get; set; }
        public string CustomerImpact { get; set; }
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime? EndTime { get; set; }
        public DateTime NotifiedTime { get; set; } = DateTime.UtcNow;
        public Severity Severity { get; set; }
        public Status Status { get; set; }
        public Bridge Bridge { get; set; }
    }
}
