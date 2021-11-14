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

namespace IoT.IncidentManagement.ClientApp.Features.Statuses.Commands.Get
{
    public class GetStatusListHandler : IRequestHandler<GetStatusListRequest, IEnumerable<Status>>
    {

        private readonly IStatusClient client;

        public GetStatusListHandler(IStatusClient client)
        {
            this.client = client;
        }

        public Task<IEnumerable<Status>> Handle(GetStatusListRequest request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new BadRequestException(nameof(request));

            return client.GetAllStatusesAsync(cancellationToken);
        }
    }
}
