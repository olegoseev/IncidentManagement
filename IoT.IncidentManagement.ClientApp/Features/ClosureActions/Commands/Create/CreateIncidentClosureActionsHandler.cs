using AutoMapper;

using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientApp.Exceptions;
using IoT.IncidentManagement.ClientApp.Models;
using IoT.IncidentManagement.ClientDomain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.ClosureActions.Commands.Create
{
    public class CreateIncidentClosureActionsHandler : IRequestHandler<CreateIncidentClosureActionsRequest, ClosureAction>
    {
        private readonly IClosureActionClient client;
        private readonly IMapper mapper;

        public CreateIncidentClosureActionsHandler(IClosureActionClient client, IMapper mapper)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public Task<ClosureAction> Handle(CreateIncidentClosureActionsRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new BadRequestException(nameof(request));

            var dto = mapper.Map<ClosureActionDto>(request);
            return client.AddClosureActionAsync(dto, cancellationToken);
        }
    }
}
