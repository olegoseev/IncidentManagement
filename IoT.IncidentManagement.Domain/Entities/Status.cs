using IoT.IncidentManagement.Domain.Common;

namespace IoT.IncidentManagement.Domain.Entities
{
    public class Status : AuditEntity
    {
        public int Id { get; set; }
        public string CurrentStatus { get; set; }
    }
}
