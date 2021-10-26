using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Application.Responses;

namespace IoT.IncidentManagement.Application.Features.Bridges.Commands.Create
{
    public class CreateBridgeResponse : BaseResponse
    {
        public CreateBridgeResponse() : base() { }

        public CreateBridgeResponse(string message) : base(message) { }

        public CreateBridgeResponse(string message, bool success) : base(message, success) { }

        public BridgeDto Bridge { get; set; }
    }
}
