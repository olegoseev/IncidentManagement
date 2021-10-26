using IoT.IncidentManagement.ClientApp.Features.Notifications.Get.EnabledTypes;
using IoT.IncidentManagement.ClientApp.Models;
using IoT.IncidentManagement.ClientDomain.Entities;
using IoT.IncidentManagement.ClientDomain.Enum;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Contracts
{
    public interface INotificationClient
    {
        public Task<EnabledNotificationTypes> GetEnabledNotificationTypesAsync(int IncidentId, CancellationToken cancellationToken);
        public Task<IEnumerable<NotificationDto>> GetIncidentNotificationsAsync(int IncidentId, CancellationToken cancellationToken);
        public Task AddGroupAsync(NotificationGroupDto body, CancellationToken cancellationToken);
        public Task DeleteGroupAsync(NotificationGroupDto body, CancellationToken cancellationToken);
        public Task UpdateNotificationAsync(NotificationDto body, CancellationToken cancellationToken);
        public Task<NotificationDto> GetNotificationAsync(int id, CancellationToken cancellationToken);
        public Task<IEnumerable<NotificationDto>> GetIncidentNotificationGroupAsync(int incidentId, NotificationGroup group, CancellationToken cancellationToken);
    }
}
