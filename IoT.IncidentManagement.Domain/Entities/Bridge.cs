using IoT.IncidentManagement.Domain.Common;

using System.ComponentModel.DataAnnotations;

namespace IoT.IncidentManagement.Domain.Entities
{
    public class Bridge : AuditEntity
    {
        public int Id { get; set; }
        public string BridgeType { get; set; }
    }
}
