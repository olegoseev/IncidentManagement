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

namespace IoT.IncidentManagement.ClientApp.Features.Notifications.Update
{
    public class UpdateNotificationHandler : IRequestHandler<UpdateNotificationRequest>
    {
        private readonly INotificationClient client;
        private readonly IMapper mapper;

        public UpdateNotificationHandler(INotificationClient client, IMapper mapper)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateNotificationRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new BadRequestException(nameof(request));

            var validator = new UpdateNotificationValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid is false)
                throw new ValidationException(validationResult);


            var dto = mapper.Map<NotificationDto>(request);
            await client.UpdateNotificationAsync(dto, cancellationToken);
            return Unit.Value;
        }
    }
}
