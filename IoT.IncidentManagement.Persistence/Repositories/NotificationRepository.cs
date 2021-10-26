using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;
using IoT.IncidentManagement.Domain.Enums;
using IoT.IncidentManagement.Persistence.Context;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Persistence.Repositories
{
    public class NotificationRepository : AppRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(IncidentManagementDbContext dbContext) : base(dbContext) { }

        public async Task DeleteGroupAsync(int incidentId, NotificationGroup group)
        {
            var notifications = await dbContext.Notifications
                .Where(n => n.IncidentId == incidentId && n.Group == group)
                .ToListAsync();

            foreach (var notification in notifications)
            {
                await DeleteAsync(notification);
            }
        }

        public async Task<IEnumerable<Notification>> GetIncidentNotificationGroupAsync(int incidentId, NotificationGroup group)
        {
            var entity = await dbContext.Notifications.Where(n => n.IncidentId == incidentId && n.Group == group).ToListAsync();
            return entity;
        }

        public async Task<IEnumerable<Notification>> GetIncidentNotificationsAsync(int incidentId)
        {
            return await dbContext.Notifications.Where(n => n.IncidentId == incidentId).ToListAsync();
        }

        public async Task<IncidentNotificationGroup> GetIncidentRegisteredNotificationGroupsAsync(int incidentId)
        {
            var internlEnabled = await dbContext.Notifications
                .Where(n => n.IncidentId == incidentId && n.Group == NotificationGroup.INTERNAL)
                .AnyAsync();
            var externalEnabled = await dbContext.Set<Notification>()
                .Where(n => n.IncidentId == incidentId && n.Group == NotificationGroup.EXTERNAL)
                .AnyAsync();

            return new IncidentNotificationGroup
            {
                InternalNotificationEnabled = internlEnabled,
                ExternalNotificationEnabled = externalEnabled
            };
        }
    }
}
