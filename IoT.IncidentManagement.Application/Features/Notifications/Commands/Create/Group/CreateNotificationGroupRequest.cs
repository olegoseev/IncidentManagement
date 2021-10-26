using IoT.IncidentManagement.Domain.Enums;

using MediatR;

using System;

namespace IoT.IncidentManagement.Application.Features.Notifications.Commands.Create.Group
{
    public class CreateNotificationGroupRequest : IRequest
    {
        public int IncidentId { get; set; }
        public NotificationGroup Group { get; set; }
        public int Interval { get; set; }
        public DateTime InitTime { get; set; }
    }
}
