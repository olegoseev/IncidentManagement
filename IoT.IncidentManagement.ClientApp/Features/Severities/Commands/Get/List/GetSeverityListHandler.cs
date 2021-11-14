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

namespace IoT.IncidentManagement.ClientApp.Features.Severities.Commands.Get.List
{
    public class GetSeverityListHandler : IRequestHandler<GetSeverityListRequest, IEnumerable<Severity>>
    {

        private readonly ISeverityClient client;

        public GetSeverityListHandler(ISeverityClient client)
        {
            this.client = client;
        }

        public Task<IEnumerable<Severity>> Handle(GetSeverityListRequest request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new BadRequestException(nameof(request));

            return client.GetAllSeveritiesAsync(cancellationToken);
        }
    }
}
