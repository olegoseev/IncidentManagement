using IoT.IncidentManagement.ClientDomain.Enum;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Notifications.Delete
{
    public class DeleteNotificationGroupRequest : IRequest
    {
        public int IncidentId { get; set; }
        public NotificationGroup Group { get; set; }
    }
}
