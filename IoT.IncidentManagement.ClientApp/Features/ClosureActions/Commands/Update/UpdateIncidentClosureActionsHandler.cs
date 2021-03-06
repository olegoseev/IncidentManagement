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

namespace IoT.IncidentManagement.ClientApp.Features.ClosureActions.Commands.Update
{
    public class UpdateIncidentClosureActionsHandler : IRequestHandler<UpdateIncidentClosureActionsRequest>
    {
        private readonly IClosureActionClient client;
        private readonly IMapper mapper;

        public UpdateIncidentClosureActionsHandler(IClosureActionClient client, IMapper mapper)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateIncidentClosureActionsRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new BadRequestException(nameof(request));

            var dto = mapper.Map<ClosureActionDto>(request);
            await client.UpdateClosureActionAsync(dto, cancellationToken);
            return Unit.Value;
        }
    }
}
