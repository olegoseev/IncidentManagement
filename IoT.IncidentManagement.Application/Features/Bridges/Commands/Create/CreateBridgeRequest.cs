
using IoT.IncidentManagement.Application.Models;

using MediatR;

namespace IoT.IncidentManagement.Application.Features.Bridges.Commands.Create
{
    public class CreateBridgeRequest : IRequest<BridgeDto>
    {
        public string BridgeType { get; set; }
    }
}
