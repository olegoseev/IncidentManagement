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

namespace IoT.IncidentManagement.ClientApp.Features.Incidents.Commands.Get.List
{
    public class GetIncidentListHandler : IRequestHandler<GetIncidentListRequest, IEnumerable<Incident>>
    {

        private readonly IIncidentClient _client;

        public GetIncidentListHandler(IIncidentClient client)
        {
            _client = client;
        }

        public Task<IEnumerable<Incident>> Handle(GetIncidentListRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            return _client.GetAllIncidentsAsync(cancellationToken);
        }
    }
}
