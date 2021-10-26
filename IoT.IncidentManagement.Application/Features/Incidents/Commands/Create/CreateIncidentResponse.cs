using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Application.Responses;

namespace IoT.IncidentManagement.Application.Features.Incidents.Commands.Create
{
    public class CreateIncidentResponse : BaseResponse
    {
        public CreateIncidentResponse()
            : base()
        {

        }

        public CreateIncidentResponse(string message)
            : base(message)
        {

        }

        public CreateIncidentResponse(string message, bool success)
            : base(message, success)
        {

        }
        public IncidentDto Incident { get; set; }
    }
}
