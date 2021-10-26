using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.ManagerActions.Commands.Create.One
{
    public class CreateManagerActionHandler : IRequestHandler<CreateManagerActionRequest, ManagerActionDto>
    {
        private readonly IManagerActionRepository actionRepository;
        private readonly IMapper mapper;

        public CreateManagerActionHandler(IManagerActionRepository actionRepository, IMapper mapper)
        {
            this.actionRepository = actionRepository;
            this.mapper = mapper;
        }

        public async Task<ManagerActionDto> Handle(CreateManagerActionRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new BadRequestException(nameof(request));

            var validator = new CreateManagerActionValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid is false)
                throw new ValidationException(validationResult);

            var action = mapper.Map<ManagerAction>(request);
            var actionDb = await actionRepository.AddAsync(action);
            return mapper.Map<ManagerActionDto>(actionDb);
        }
    }
}
