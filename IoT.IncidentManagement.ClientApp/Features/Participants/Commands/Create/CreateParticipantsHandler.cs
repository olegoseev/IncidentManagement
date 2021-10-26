using AutoMapper;

using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientApp.Exceptions;
using IoT.IncidentManagement.ClientApp.Models;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Participants.Commands.Create
{
    public class CreateParticipantsHandler :IRequestHandler<CreateParticipantsRequest>
    {
        private readonly IParticipantClient client;
        private readonly IMapper mapper;

        public CreateParticipantsHandler(IParticipantClient client, IMapper mapper)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(CreateParticipantsRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new BadRequestException(nameof(request));

            var validator = new CreateParticipantsValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid is false)
                throw new ValidationException(validationResult);

            var dto = mapper.Map<ParticipantsDto>(request);

            await client.CreateParticipantsAsync(dto, cancellationToken);
            return Unit.Value;
        }
    }
}
