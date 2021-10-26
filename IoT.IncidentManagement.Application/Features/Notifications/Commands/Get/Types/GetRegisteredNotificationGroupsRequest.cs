using IoT.IncidentManagement.Application.Models;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Notifications.Commands.Get.Types
{
    public class GetRegisteredNotificationGroupsRequest : IRequest<IncidentNotificationGroup>
    {
        public int IncidentId { get; set; }
    }
}
