using IoT.IncidentManagement.Domain.Common;

namespace IoT.IncidentManagement.Domain.Entities
{
    public class Severity : AuditEntity
    {
        public int Id { get; set; }
        public string IncidentSeverity { get; set; }
        public int NotificationInterval { get; set; }
    }
}
