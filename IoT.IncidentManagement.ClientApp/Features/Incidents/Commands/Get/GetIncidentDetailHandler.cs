using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientApp.Exceptions;
using IoT.IncidentManagement.ClientDomain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Incidents.Commands.Get
{
    public class GetIncidentDetailHandler : IRequestHandler<GetIncidentDetailRequest, Incident>
    {

        private readonly IIncidentClient client;

        public GetIncidentDetailHandler(IIncidentClient client)
        {
            this.client = client;
        }

        public Task<Incident> Handle(GetIncidentDetailRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            return client.GetIncidentByIdAsync(request.IncidentId, cancellationToken);
        }
    }
}
