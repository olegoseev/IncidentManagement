using IoT.IncidentManagement.Domain.Entities;
using IoT.IncidentManagement.Domain.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Contracts.Persistence
{
    public interface INotificationStoreRepository : IAppRepository<NotificationStore>
    {
        Task<IEnumerable<NotificationStore>> GetAllOfGroup(NotificationGroup group);
    }
}
