using IoT.IncidentManagement.Application.Models;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Notifications.Commands.Get.One
{
    public class GetNotificationRequest: IRequest<NotificationDto>
    {
        public int Id { get; set; }
    }
}
