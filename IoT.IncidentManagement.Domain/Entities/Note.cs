using IoT.IncidentManagement.Domain.Common;

using System;

namespace IoT.IncidentManagement.Domain.Entities
{
    public class Note : AuditEntity
    {
        public int Id { get; set; }
        public DateTime RecordTime {  get; set; }
        public string Record { get; set; }
        public int IncidentId { get; set; }
        public Incident Incident { get; set; }
    }
}
