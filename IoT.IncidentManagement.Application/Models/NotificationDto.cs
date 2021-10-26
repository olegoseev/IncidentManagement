using IoT.IncidentManagement.Domain.Entities;
using IoT.IncidentManagement.Domain.Enums;

using System;

namespace IoT.IncidentManagement.Application.Models
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public int IncidentId { get; set; }
        public NotificationType Type { get; set; }
        public NotificationGroup Group { get; set; }
        public int Order { get; set; }
        public int Interval { get; set; }
        public bool Repeat { get; set; }
        public DateTime InitTime { get; set; }
        public DateTime SentTime { get; set; }
        public NotificationState State { get; set; }
    }
}
