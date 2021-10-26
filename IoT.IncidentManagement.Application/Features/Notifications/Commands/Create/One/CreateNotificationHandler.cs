using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Notifications.Commands.Create.One
{
    public class CreateNotificationHandler : IRequestHandler<CreateNotificationRequest, NotificationDto>
    {
        private readonly INotificationRepository notificationRepository;
        private readonly IMapper mapper;

        public CreateNotificationHandler(INotificationRepository notificationRepository, IMapper mapper)
        {
            this.notificationRepository = notificationRepository;
            this.mapper = mapper;
        }

        public async Task<NotificationDto> Handle(CreateNotificationRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new BadRequestException(nameof(request));

            var validator = new CreateNotificationValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid is false)
                throw new ValidationException(validationResult);


            var notification = mapper.Map<Notification>(request);
            notification.IncidentId = request.IncidentId;
            notification = await notificationRepository.AddAsync(notification);
            return mapper.Map<NotificationDto>(notification);
        }
    }
}
