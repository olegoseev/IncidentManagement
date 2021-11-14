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

        private readonly IIncidentClient client;

        public GetIncidentListHandler(IIncidentClient client)
        {
            this.client = client;
        }

        public Task<IEnumerable<Incident>> Handle(GetIncidentListRequest request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new BadRequestException(nameof(request));

            return client.GetAllIncidentsAsync(cancellationToken);
        }
    }
}
