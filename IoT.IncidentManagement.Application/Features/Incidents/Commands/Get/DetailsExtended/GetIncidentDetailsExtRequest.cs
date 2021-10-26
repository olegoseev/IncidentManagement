using IoT.IncidentManagement.Application.Models;

using MediatR;

namespace IoT.IncidentManagement.Application.Features.Incidents.Commands.Get.DetailsExtended
{
    public class GetIncidentDetailsExtRequest : IRequest<IncidentExtDto>
    {
        public int Id { get; set; }
    }
}
