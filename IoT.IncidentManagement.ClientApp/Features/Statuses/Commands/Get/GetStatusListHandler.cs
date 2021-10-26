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

        private readonly IStatusClient _client;

        public GetStatusListHandler(IStatusClient client)
        {
            _client = client;
        }

        public Task<IEnumerable<Status>> Handle(GetStatusListRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            return _client.GetAllStatusesAsync(cancellationToken);
        }
    }
}
