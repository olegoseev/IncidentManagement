
using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Update
{
    public class UpdateClosureActionHandler : IRequestHandler<UpdateClosureActionRequest>
    {
        private readonly IAppRepository<ClosureAction> repository;
        private readonly IMapper mapper;

        public UpdateClosureActionHandler(IAppRepository<ClosureAction> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateClosureActionRequest request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new BadRequestException(nameof(request));

            var validator = new UpdateClosureActionValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if(validationResult.IsValid is false)
                throw new ValidationException(validationResult);

            var entity = await repository.GetByIdAsync(request.IncidentId);
            
            if(entity is null)
                throw new NotFoundException(nameof(entity));

            mapper.Map(request, entity,typeof(UpdateClosureActionRequest), typeof(ClosureAction));

            await repository.UpdateAsync(entity);

            return Unit.Value;
        }
    }
}
