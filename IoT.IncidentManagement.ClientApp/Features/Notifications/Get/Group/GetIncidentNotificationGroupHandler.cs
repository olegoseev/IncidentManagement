using AutoMapper;

using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientApp.Exceptions;
using IoT.IncidentManagement.ClientApp.Models;
using IoT.IncidentManagement.ClientDomain.Entities;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Notifications.Get.Group
{
    public class GetIncidentNotificationGroupHandler : IRequestHandler<GetIncidentNotificationGroupRequest, IEnumerable<Notification>>
    {
        private readonly INotificationClient client;
        private readonly IMapper mapper;

        public GetIncidentNotificationGroupHandler(INotificationClient client, IMapper mapper)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Notification>> Handle(GetIncidentNotificationGroupRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new BadRequestException(nameof(request));

            IEnumerable<NotificationDto> dto = await client.GetIncidentNotificationGroupAsync(request.IncidentId, request.Group, cancellationToken);
            var entity = mapper.Map<IEnumerable<Notification>>(dto);
            return entity;
        }
    }
}
