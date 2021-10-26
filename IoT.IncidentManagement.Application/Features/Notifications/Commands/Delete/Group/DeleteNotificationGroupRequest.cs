using IoT.IncidentManagement.Domain.Enums;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Notifications.Commands.Delete.Group
{
    public class DeleteNotificationGroupRequest : IRequest
    {
        public int IncidentId { get; set; }
        public NotificationGroup Group { get; set; }
    }
}
