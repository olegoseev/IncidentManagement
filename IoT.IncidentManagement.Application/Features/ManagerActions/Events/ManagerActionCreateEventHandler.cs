using AutoMapper;

using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.ManagerActions.Commands.Create.One;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.ManagerActions.Events
{
    public class ManagerActionCreateEventHandler : INotificationHandler<ManagerActionCreateEvent>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public ManagerActionCreateEventHandler(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task Handle(ManagerActionCreateEvent notification, CancellationToken cancellationToken)
        {
            if(notification is null)
                throw new BadRequestException(nameof(notification));

            var validator = new ManagerActionCreateEventValidator(); 

            var validationResult = await validator.ValidateAsync(notification, cancellationToken);
            if (validationResult.IsValid is false)
                throw new ValidationException(validationResult);

            var request = mapper.Map<CreateManagerActionRequest>(notification);

            if(request is null)
                throw new BadRequestException(nameof(CreateManagerActionRequest));

            await mediator.Send(request, cancellationToken);
        }
    }
}
