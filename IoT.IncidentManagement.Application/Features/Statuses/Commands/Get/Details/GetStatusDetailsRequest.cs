using IoT.IncidentManagement.Application.Models;

using MediatR;

namespace IoT.IncidentManagement.Application.Features.Statuses.Commands.Get.Details
{
    public class GetStatusDetailsRequest : IRequest<StatusDto>
    {
        public int Id { get; set; }
    }
}
