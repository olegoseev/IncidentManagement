
using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Notifications.Get.EnabledTypes
{
    public class GetEnabledNotificationTypesRequest : IRequest<EnabledNotificationTypes>
    {
        public int IncidentId { get; set; }
    }
}
