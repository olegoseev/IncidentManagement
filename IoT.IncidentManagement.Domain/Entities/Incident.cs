using IoT.IncidentManagement.Domain.Common;

using System;
using System.Collections.Generic;

namespace IoT.IncidentManagement.Domain.Entities
{
    public class Incident : AuditEntity
    {
        public int Id { get; set; }
        public string IncidentCase { get; set; }
        public string Description { get; set; }
        public string CustomerImpact { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime NotifiedTime { get; set; }
        public Status Status { get; set; }
        public int StatusId { get; set; }
        public Severity Severity { get; set; }
        public int SeverityId { get; set; }
        public Bridge Bridge { get; set; }
        public int BridgeId { get; set; }
        public ClosureAction ClosureAction { get; set; }
        public Participant Participant { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Note> Notes { get; set; }
    }
}
