using AutoMapper;

using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientApp.Exceptions;
using IoT.IncidentManagement.ClientApp.Models;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Notifications.Delete
{
    public class DeleteNotificationGroupHandler : IRequestHandler<DeleteNotificationGroupRequest>
    {

        private readonly INotificationClient client;
        private readonly IMapper mapper;

        public DeleteNotificationGroupHandler(INotificationClient client, IMapper mapper)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteNotificationGroupRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new BadRequestException(nameof(request));

            var dto = mapper.Map<NotificationGroupDto>(request);
            await client.DeleteGroupAsync(dto, cancellationToken);
            return Unit.Value;
        }
    }
}
