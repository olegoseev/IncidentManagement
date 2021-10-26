using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientApp.Features.Notifications.Get.EnabledTypes;
using IoT.IncidentManagement.ClientApp.Models;
using IoT.IncidentManagement.ClientDomain.Enum;

using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientServices.Services
{
    public class NotificationClient : AppClient, INotificationClient
    {
        public NotificationClient(HttpClient httpClient) : base(httpClient)
        {

        }

        public Task AddGroupAsync(NotificationGroupDto body, CancellationToken cancellationToken)
        {
            URL = $"api/notification/group";
            return AddAsync(body, cancellationToken);
        }

        public Task DeleteGroupAsync(NotificationGroupDto body, CancellationToken cancellationToken)
        {
            URL = $"api/notification/{body.IncidentId}/group/{body.Group}";
            return DeleteAsync(cancellationToken);
        }

        public Task<EnabledNotificationTypes> GetEnabledNotificationTypesAsync(int IncidentId, CancellationToken cancellationToken)
        {
            URL = $"api/notification/{IncidentId}/registered";
            return GetAsync<EnabledNotificationTypes>(cancellationToken);
        }

        public Task<IEnumerable<NotificationDto>> GetIncidentNotificationGroupAsync(int incidentId, NotificationGroup group, CancellationToken cancellationToken)
        {
            URL = $"api/notification/{incidentId}/group/{group}";
            return GetAsync<IEnumerable<NotificationDto>>(cancellationToken);
        }

        public Task<IEnumerable<NotificationDto>> GetIncidentNotificationsAsync(int IncidentId, CancellationToken cancellationToken)
        {
            URL = $"api/notification/{IncidentId}/all";
            return GetAsync<IEnumerable<NotificationDto>>(cancellationToken);
        }

        public Task<NotificationDto> GetNotificationAsync(int id, CancellationToken cancellationToken)
        {
            URL = $"api/notification/{id}";
            return GetAsync<NotificationDto>(cancellationToken);
        }

        public Task UpdateNotificationAsync(NotificationDto body, CancellationToken cancellationToken)
        {
            URL = $"api/notification/";
            return UpdateAsync(body, cancellationToken);
        }
    }
}
