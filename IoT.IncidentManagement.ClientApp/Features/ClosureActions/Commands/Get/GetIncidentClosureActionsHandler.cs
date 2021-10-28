using AutoMapper;

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

namespace IoT.IncidentManagement.ClientApp.Features.ClosureActions.Commands.Get
{
    public class GetIncidentClosureActionsHandler : IRequestHandler<GetIncidentClosureActionsRequest, ClosureAction>
    {
        private readonly IClosureActionClient client;
        private readonly IMapper mapper;

        public GetIncidentClosureActionsHandler(IClosureActionClient client, IMapper mapper)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public async Task<ClosureAction> Handle(GetIncidentClosureActionsRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new BadRequestException(nameof(request));

            var dto = await client.GetClosureActionAsync(request.IncidentId, cancellationToken);
            return mapper.Map<ClosureAction>(dto);
        }
    }
}
