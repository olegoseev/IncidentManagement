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

namespace IoT.IncidentManagement.ClientApp.Features.Notifications.Get.One
{
    public class GetNotificationHandler : IRequestHandler<GetNotificationRequest, Notification>
    {
        private readonly INotificationClient client;
        private readonly IMapper mapper;

        public GetNotificationHandler(INotificationClient client, IMapper mapper)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public async Task<Notification> Handle(GetNotificationRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new BadRequestException(nameof(request));

            var dto = await client.GetNotificationAsync(request.Id, cancellationToken);
            return mapper.Map<Notification>(dto);
        }
    }
}
