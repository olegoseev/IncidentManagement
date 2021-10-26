using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Enums;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Notifications.Commands.Get.List
{
    public class GetNotificationListRequest : IRequest<IEnumerable<NotificationDto>>
    {
        public int IncidentId { get; set; }
    }
}
