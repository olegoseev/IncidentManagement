using System;
using System.ComponentModel.DataAnnotations;

namespace IoT.IncidentManagement.Domain.Common
{
    public class AuditEntity
    {
        [StringLength(50)]
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
