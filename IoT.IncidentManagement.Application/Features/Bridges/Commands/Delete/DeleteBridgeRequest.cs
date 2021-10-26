using MediatR;

namespace IoT.IncidentManagement.Application.Features.Bridges.Commands.DeleteBridge
{
    public class DeleteBridgeRequest : IRequest
    {
        public int Id { get; set; }
    }
}
