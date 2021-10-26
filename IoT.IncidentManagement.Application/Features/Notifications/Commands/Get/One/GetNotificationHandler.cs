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

namespace IoT.IncidentManagement.Application.Features.Notifications.Commands.Get.One
{
    public class GetNotificationHandler : IRequestHandler<GetNotificationRequest, NotificationDto>
    {
        private readonly INotificationRepository notificationRepository;
        private readonly IMapper mapper;

        public GetNotificationHandler(INotificationRepository notificationRepository, IMapper mapper)
        {
            this.notificationRepository = notificationRepository;
            this.mapper = mapper;
        }

        public async Task<NotificationDto> Handle(GetNotificationRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new BadRequestException(nameof(request));


            var notification = await notificationRepository.GetByIdAsync(request.Id);

            if (notification is null)
                throw new NotFoundException(nameof(notification));

            return mapper.Map<NotificationDto>(notification);
        }
    }
}
