using IoT.IncidentManagement.Domain.Common;

namespace IoT.IncidentManagement.Domain.Entities
{
    public class Participant : AuditEntity
    {
        public int IncidentId { get; set; }
        public string Group { get; set; }
        public Incident Incident { get; set; }
    }
}
