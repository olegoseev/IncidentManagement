using IoT.IncidentManagement.Application.Models;

using MediatR;

namespace IoT.IncidentManagement.Application.Features.Severities.Commands.Get.Details
{
    public class GetSeverityDetailsRequest : IRequest<SeverityDto>
    {
        public int Id { get; set; }
    }
}
