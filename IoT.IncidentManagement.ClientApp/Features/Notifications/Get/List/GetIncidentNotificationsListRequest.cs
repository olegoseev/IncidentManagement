using IoT.IncidentManagement.ClientDomain.Entities;

using MediatR;

using System.Collections.Generic;

namespace IoT.IncidentManagement.ClientApp.Features.Notifications.Get.List
{
    public class GetIncidentNotificationsListRequest : IRequest<IEnumerable<Notification>>
    {
        public int IncidentId { get; set; }
    }
}
