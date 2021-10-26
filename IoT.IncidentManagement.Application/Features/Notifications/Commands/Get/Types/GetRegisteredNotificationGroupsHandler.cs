using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Notifications.Commands.Get.Types
{
    public class GetRegisteredNotificationGroupsHandler : IRequestHandler<GetRegisteredNotificationGroupsRequest, IncidentNotificationGroup>
    {
        private readonly INotificationRepository repository;

        public GetRegisteredNotificationGroupsHandler(INotificationRepository repository)
        {
            this.repository = repository;
        }

        public Task<IncidentNotificationGroup> Handle(GetRegisteredNotificationGroupsRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new BadRequestException(nameof(request));

            return repository.GetIncidentRegisteredNotificationGroupsAsync(request.IncidentId);
        }
    }
}
