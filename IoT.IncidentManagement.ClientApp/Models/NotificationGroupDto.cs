using IoT.IncidentManagement.ClientDomain.Enum;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Models
{
    public class NotificationGroupDto
    {
        public int IncidentId { get; set; }
        public NotificationGroup Group { get; set; }
        public int Interval { get; set; }
        public DateTime InitTime { get; set; }
    }
}
