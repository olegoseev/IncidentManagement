using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Domain.Enums
{
    public enum NotificationState
    {
        INITIAL = 1,
        WAITING,
        WARNING,
        ALARM,
        OFF
    }
}
