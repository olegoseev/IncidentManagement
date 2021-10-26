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

namespace IoT.IncidentManagement.ClientApp.Features.Bridges.Commands.Get.List
{
    public class GetBridgesListHandler : IRequestHandler<GetBridgesListRequest, IEnumerable<Bridge>>
    {
        private readonly IBridgeClient _client;
        public GetBridgesListHandler(IBridgeClient client)
        {
            _client = client;
        }

        public Task<IEnumerable<Bridge>> Handle(GetBridgesListRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));
            
            return _client.GetAllBridgesAsync(cancellationToken);
        }
    }
}
