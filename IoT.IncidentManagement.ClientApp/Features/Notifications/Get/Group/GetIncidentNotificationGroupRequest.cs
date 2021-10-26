using IoT.IncidentManagement.ClientDomain.Entities;
using IoT.IncidentManagement.ClientDomain.Enum;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Notifications.Get.Group
{
    public class GetIncidentNotificationGroupRequest :IRequest<IEnumerable<Notification>>
    {
        public int IncidentId { get; set; }
        public NotificationGroup Group {  get; set; }
    }
}
