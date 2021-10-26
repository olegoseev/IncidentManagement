using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientApp.Exceptions;
using IoT.IncidentManagement.ClientDomain.Entities;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Participants.Commands.Get
{
    public class GetParticipantsHandler : IRequestHandler<GetParticipantsRequest, Participant>
    {

        private readonly IParticipantClient _client;

        public GetParticipantsHandler(IParticipantClient client)
        {
            _client = client;
        }

        public Task<Participant> Handle(GetParticipantsRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));
            return _client.GetIncidentParticipantsByIncidentIdAsync(request.IncidentId, cancellationToken);
        }
    }
}
