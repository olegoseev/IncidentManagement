using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;
using IoT.IncidentManagement.Domain.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Contracts.Persistence
{
    public interface INotificationRepository : IAppRepository<Notification>
    {
        Task<IEnumerable<Notification>> GetIncidentNotificationsAsync(int incidentId);
        Task<IncidentNotificationGroup> GetIncidentRegisteredNotificationGroupsAsync(int incidentId);
        Task DeleteGroupAsync(int incidentId, NotificationGroup group);
        Task<IEnumerable<Notification>> GetIncidentNotificationGroupAsync(int incidentId, NotificationGroup group);
    }
}
