using IoT.IncidentManagement.Application.Models;

using MediatR;

namespace IoT.IncidentManagement.Application.Features.Incidents.Commands.Get.Details
{
    public class GetIncidentDetailsRequest : IRequest<IncidentDto>
    {
        public int Id { get; set; }
    }
}
