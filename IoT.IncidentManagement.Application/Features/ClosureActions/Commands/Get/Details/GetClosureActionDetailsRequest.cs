using IoT.IncidentManagement.Application.Models;

using MediatR;

namespace IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Get.Details
{
    public class GetClosureActionDetailsRequest : IRequest<ClosureActionDto>
    {
        public int Id { get; set; }
    }
}
