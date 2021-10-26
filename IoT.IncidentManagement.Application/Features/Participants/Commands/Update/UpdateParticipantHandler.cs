using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Participants.Commands.Update
{
    public class UpdateParticipantHandler : IRequestHandler<UpdateParticipantRequest>
    {
        private readonly IAppRepository<Participant> repository;
        private readonly IMapper mapper;

        public UpdateParticipantHandler(IAppRepository<Participant> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateParticipantRequest request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new BadRequestException(nameof(request));

            var entity = await repository.GetByIdAsync(request.IncidentId);
            if(entity is null)
                throw new NotFoundException(nameof(Participant), request.IncidentId);

            var validator = new UpdateParticipantValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if(validationResult.IsValid is false)
                throw new ValidationException(validationResult);

            mapper.Map(request, entity, typeof(UpdateParticipantRequest), typeof(Participant));
            await repository.UpdateAsync(entity);

            return Unit.Value;
        }
    }
}
