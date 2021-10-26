using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Severities.Commands.Update
{
    public class UpdateSeverityHandler : IRequestHandler<UpdateSeverityRequest>
    {
        private readonly ISeverityRepository repository;
        private readonly IMapper mapper;

        public UpdateSeverityHandler(ISeverityRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateSeverityRequest request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new BadRequestException(nameof(request));

            var entity = await repository.GetByIdAsync(request.Id);
            if(entity is null)
                throw new NotFoundException(nameof(Severity), request.Id);

            var validator = new UpdateSeverityValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if(validationResult.IsValid is false)
                throw new ValidationException(validationResult);

            mapper.Map(request, entity, typeof(UpdateSeverityRequest), typeof(Severity));
            await repository.UpdateAsync(entity);

            return Unit.Value;
        }
    }
}
