using IoT.IncidentManagement.Domain.Enums;

using MediatR;

using System;

namespace IoT.IncidentManagement.Application.Features.ManagerActions.Events
{
    public class ManagerActionCreateEvent : INotification
    {
        public int IncidentId { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public int Interval { get; set; }
        public bool Repeat { get; set; }
        public DateTime InitTime { get; set; }
        public NotificationState? State { get; set; }
    }
}
