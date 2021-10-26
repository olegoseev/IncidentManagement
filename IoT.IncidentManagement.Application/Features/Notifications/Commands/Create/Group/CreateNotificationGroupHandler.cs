
using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Notifications.Events;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Notifications.Commands.Create.Group
{
    public class CreateNotificationGroupHandler : IRequestHandler<CreateNotificationGroupRequest>
    {
        private readonly INotificationStoreRepository notificationStoreRepository;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CreateNotificationGroupHandler(INotificationStoreRepository notificationStoreRepository, IMediator mediator, IMapper mapper)
        {
            this.notificationStoreRepository = notificationStoreRepository;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(CreateNotificationGroupRequest request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new BadRequestException(nameof(request));

            var notifications = await notificationStoreRepository.GetAllOfGroup(request.Group);

            foreach (var notification in notifications)
            {
                var createEvent = mapper.Map<CreateNotificationEvent>(notification);
                createEvent.IncidentId = request.IncidentId;
                createEvent.InitTime = request.InitTime;
                await mediator.Publish(createEvent, cancellationToken);
            }

            return Unit.Value;
        }
    }
}
