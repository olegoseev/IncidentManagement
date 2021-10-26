using MediatR;

namespace IoT.IncidentManagement.Application.Features.Bridges.Commands.UpdateBridge
{
    public class UpdateBridgeRequest : IRequest
    {
        public int Id { get; set; }
        public string BridgeType { get; set; }
    }
}
