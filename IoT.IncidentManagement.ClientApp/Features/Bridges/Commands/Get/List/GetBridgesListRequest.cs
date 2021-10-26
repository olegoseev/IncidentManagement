using IoT.IncidentManagement.ClientDomain.Entities;

using MediatR;

using System.Collections.Generic;

namespace IoT.IncidentManagement.ClientApp.Features.Bridges.Commands.Get.List
{
    public class GetBridgesListRequest : IRequest<IEnumerable<Bridge>>
    {

    }
}
