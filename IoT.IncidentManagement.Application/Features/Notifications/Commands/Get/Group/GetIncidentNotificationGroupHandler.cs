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

namespace IoT.IncidentManagement.Application.Features.Notifications.Commands.Get.Group
{
    public class GetIncidentNotificationGroupHandler : IRequestHandler<GetIncidentNotificationGroupRequest, IEnumerable<NotificationDto>>
    {
        private readonly INotificationRepository repository;
        private readonly IMapper mapper;

        public GetIncidentNotificationGroupHandler(INotificationRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<NotificationDto>> Handle(GetIncidentNotificationGroupRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new BadRequestException(nameof(request));


            var entity = await repository.GetIncidentNotificationGroupAsync(request.IncidentId, request.Group);

            if (entity is null)
                throw new NotFoundException(nameof(entity));

            return mapper.Map<IEnumerable<NotificationDto>>(entity);
        }
    }
}
