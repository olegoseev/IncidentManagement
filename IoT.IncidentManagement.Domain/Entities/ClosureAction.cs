using IoT.IncidentManagement.Domain.Common;

namespace IoT.IncidentManagement.Domain.Entities
{
    public class ClosureAction : AuditEntity
    {
        public int IncidentId { get; set; }
        public string ToDoList { get; set; }
        public Incident Incident { get; set; }
    }
}
