using IoT.IncidentManagement.Domain.Common;
using IoT.IncidentManagement.Domain.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Domain.Entities
{
    public class Notification : AuditEntity
    {
        public int Id {  get; set; }
        public int IncidentId { get; set; }
        public Incident Incident { get; set; }
        public NotificationType Type { get; set; }
        public NotificationGroup Group {  get; set; }
        public int Order { get; set; }
        public int Interval { get; set; }
        public bool Repeat { get; set; }
        public DateTime InitTime { get; set; }
        public DateTime SentTime { get; set; }
        public NotificationState State { get; set; }
    }
}
