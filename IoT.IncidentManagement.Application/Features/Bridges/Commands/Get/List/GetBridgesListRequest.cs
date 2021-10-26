using IoT.IncidentManagement.Application.Models;

using MediatR;

using System.Collections.Generic;

namespace IoT.IncidentManagement.Application.Features.Bridges.Commands.Get.List
{
    public class GetBridgesListRequest : IRequest<IReadOnlyList<BridgeDto>>
    {

    }
}
