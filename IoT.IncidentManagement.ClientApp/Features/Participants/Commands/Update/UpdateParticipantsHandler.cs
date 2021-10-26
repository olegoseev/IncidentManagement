using AutoMapper;

using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientApp.Exceptions;
using IoT.IncidentManagement.ClientApp.Models;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Participants.Commands.Update
{
    public class UpdateParticipantsHandler : IRequestHandler<UpdateParticipantsRequest>
    {

        private readonly IParticipantClient client;
        private readonly IMapper mapper;

        public UpdateParticipantsHandler(IParticipantClient client, IMapper mapper)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateParticipantsRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new BadRequestException(nameof(request));

            var validator = new UpdateParticipantsValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid is false)
                throw new ValidationException(validationResult);

            var dto = mapper.Map<ParticipantsDto>(request);

            await client.UpdateParticipantsAsync(dto, cancellationToken);
            return Unit.Value;
        }
    }
}
