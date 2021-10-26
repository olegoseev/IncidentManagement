using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Domain.Entities;
using IoT.IncidentManagement.Domain.Enums;
using IoT.IncidentManagement.Persistence.Context;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Persistence.Repositories
{
    public class NotificationStoreRepository : AppRepository<NotificationStore>, INotificationStoreRepository
    {
        public NotificationStoreRepository(IncidentManagementDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<NotificationStore>> GetAllOfGroup(NotificationGroup group)
        {
            return await dbContext.Set<NotificationStore>().Where(n => n.Group == group).ToListAsync();
        }
    }
}
