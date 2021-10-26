using IoT.IncidentManagement.Application.Models;

using MediatR;

namespace IoT.IncidentManagement.Application.Features.Bridges.Commands.Get.Details
{
    public class GetBridgeDetailsRequest : IRequest<BridgeDto>
    {
        public int Id { get; set; }
    }
}
