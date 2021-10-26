using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Application.Responses;

namespace IoT.IncidentManagement.Application.Features.Statuses.Commands.Create
{
    public class CreateStatusResponse : BaseResponse
    {
        public CreateStatusResponse() : base() { }
        public CreateStatusResponse(string message) : base(message) { }
        public CreateStatusResponse(string message, bool success) : base(message, success) { }

        public StatusDto Status { get; set; }
    }
}
