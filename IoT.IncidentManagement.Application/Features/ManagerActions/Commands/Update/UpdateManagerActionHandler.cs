using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.ManagerActions.Commands.Update
{
    public class UpdateManagerActionHandler : IRequestHandler<UpdateManagerActionRequest>
    {
        private readonly IManagerActionRepository actionRepository;
        private readonly IMapper mapper;

        public UpdateManagerActionHandler(IManagerActionRepository actionRepository, IMapper mapper)
        {
            this.actionRepository = actionRepository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateManagerActionRequest request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new BadRequestException(nameof(request));

            var action = await actionRepository.GetByIdAsync(request.Id);
            if(action is null)
                throw new NotFoundException(nameof(ManagerAction), request.Id);

            var validator = new UpdateManagerActionValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if(validationResult.IsValid is false)
                throw new ValidationException(validationResult);

            mapper.Map(request, action, typeof(UpdateManagerActionRequest), typeof(ManagerAction));
            await actionRepository.UpdateAsync(action);
            return Unit.Value;
        }
    }
}
