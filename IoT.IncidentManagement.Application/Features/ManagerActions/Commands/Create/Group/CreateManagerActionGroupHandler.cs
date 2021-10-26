using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.ManagerActions.Events;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.ManagerActions.Commands.Create.Group
{
    public class CreateManagerActionGroupHandler : IRequestHandler<CreateManagerActionGroupRequest>
    {
        private readonly IActionStoreRepository actionStoreRepository;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CreateManagerActionGroupHandler(IActionStoreRepository actionStoreRepository, IMediator mediator, IMapper mapper)
        {
            this.actionStoreRepository = actionStoreRepository;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(CreateManagerActionGroupRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new BadRequestException(nameof(request));

            var actions = await actionStoreRepository.GetAllAsync();

            foreach (var action in actions)
            {
                var createEvent = mapper.Map<ManagerActionCreateEvent>(action);
                createEvent.IncidentId = request.IncidentId;
                await mediator.Publish(createEvent, cancellationToken);
            }

            return Unit.Value;
        }
    }
}
