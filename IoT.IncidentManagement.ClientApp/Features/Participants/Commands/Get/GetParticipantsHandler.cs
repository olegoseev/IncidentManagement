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

        private readonly IParticipantClient client;

        public GetParticipantsHandler(IParticipantClient client)
        {
            this.client = client;
        }

        public async Task<Participant> Handle(GetParticipantsRequest request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new BadRequestException(nameof(request));

            try
            {
                return await client.GetIncidentParticipantsByIncidentIdAsync(request.IncidentId, cancellationToken);
            }
            catch(ApiException ex)
            {
                return await Task.FromResult(new Participant { IncidentId = request.IncidentId, Group = string.Empty});
            }
        }
    }
}
