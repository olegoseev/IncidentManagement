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

namespace IoT.IncidentManagement.Application.Features.Notifications.Commands.Get.List
{
    public class GetNotificationListHandler : IRequestHandler<GetNotificationListRequest, IEnumerable<NotificationDto>>
    {
        private readonly INotificationRepository notificationRepository;
        private readonly IMapper mapper;

        public GetNotificationListHandler(INotificationRepository notificationRepository, IMapper mapper)
        {
            this.notificationRepository = notificationRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<NotificationDto>> Handle(GetNotificationListRequest request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new BadRequestException(nameof(request));

           
            var notifications = await notificationRepository.GetIncidentNotificationsAsync(request.IncidentId);

            if(notifications is null) 
                throw new NotFoundException(nameof(notifications));

            return mapper.Map<IEnumerable<NotificationDto>>(notifications);
        }
    }
}
