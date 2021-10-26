using AutoMapper;

using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Notifications.Commands.Create.One;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Notifications.Events
{
    public class CreateNotificationEventHandler : INotificationHandler<CreateNotificationEvent>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CreateNotificationEventHandler(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public Task Handle(CreateNotificationEvent notification, CancellationToken cancellationToken)
        {
            if (notification is null)
                throw new BadRequestException(nameof(notification));

            var request = mapper.Map<CreateNotificationRequest>(notification);
            if (request is null)
                throw new BadRequestException(nameof(CreateNotificationRequest));

            return mediator.Send(request, cancellationToken);
        }
    }
}
