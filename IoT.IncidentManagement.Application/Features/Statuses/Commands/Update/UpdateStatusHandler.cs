using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Statuses.Commands.Update
{
    public class UpdateStatusHandler : IRequestHandler<UpdateStatusRequest>
    {
        private readonly IAppRepository<Status> repository;
        private readonly IMapper mapper;

        public UpdateStatusHandler(IAppRepository<Status> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateStatusRequest request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new BadRequestException(nameof(request));

            var entity = await repository.GetByIdAsync(request.Id);
            if(entity is null)
                throw new NotFoundException(nameof(Status), request.Id);

            var validator = new UpdateStatusValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if(validationResult.IsValid is false)
                throw new ValidationException(validationResult);

            mapper.Map(request, entity, typeof(UpdateStatusRequest), typeof(Status));
            await repository.UpdateAsync(entity);

            return Unit.Value;
        }
    }
}
