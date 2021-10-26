
using MediatR;

namespace IoT.IncidentManagement.Application.Features.ManagerActions.Commands.Create.Group
{
    public class CreateManagerActionGroupRequest : IRequest
    {
        public int IncidentId { get; set; }
    }
}
