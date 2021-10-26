using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Notifications.Commands.Update
{
    public class UpdateNotificationHandler : IRequestHandler<UpdateNotificationRequest>
    {
        private readonly INotificationRepository notificationRepository;
        private readonly IMapper mapper;

        public UpdateNotificationHandler(INotificationRepository notificationRepository, IMapper mapper)
        {
            this.notificationRepository = notificationRepository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateNotificationRequest request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new BadRequestException(nameof(request));

            var validator = new UpdateNotificationValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if(validationResult.IsValid is false)
                throw new ValidationException(validationResult);

            var entity = await notificationRepository.GetByIdAsync(request.Id);

            mapper.Map(request, entity, typeof(UpdateNotificationRequest), typeof(Notification));
            await notificationRepository.UpdateAsync(entity);
            return Unit.Value;
        }
    }
}
