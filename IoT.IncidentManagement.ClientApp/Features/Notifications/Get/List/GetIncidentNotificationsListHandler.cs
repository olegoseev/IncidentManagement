using AutoMapper;

using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientApp.Exceptions;
using IoT.IncidentManagement.ClientDomain.Entities;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Notifications.Get.List
{
    public class GetIncidentNotificationsListHandler : IRequestHandler<GetIncidentNotificationsListRequest, IEnumerable<Notification>>
    {
        private readonly INotificationClient client;
        private readonly IMapper mapper;

        public GetIncidentNotificationsListHandler(INotificationClient client, IMapper mapper)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Notification>> Handle(GetIncidentNotificationsListRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new BadRequestException(nameof(request));

            var dto = await client.GetIncidentNotificationsAsync(request.IncidentId, cancellationToken);
            var entity = mapper.Map<IEnumerable<Notification>>(dto);
            return entity;
        }
    }
}
