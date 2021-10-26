using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientApp.Exceptions;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Incidents.Commands.Delete
{
    public class DeleteIncidentHandler : IRequestHandler<DeleteIncidentRequest>
    {
        private readonly IIncidentClient client;

        public DeleteIncidentHandler(IIncidentClient client)
        {
            this.client = client;
        }

        public async Task<Unit> Handle(DeleteIncidentRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new BadRequestException(nameof(request));
            await client.DeleteIncidentAsync(request.IncidentId, cancellationToken);
            return Unit.Value;
        }
    }
}
