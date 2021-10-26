using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Models
{
    public class IncidentNotificationGroup
    {
        public bool InternalNotificationEnabled { get; set; }
        public bool ExternalNotificationEnabled { get; set; }
    }
}
