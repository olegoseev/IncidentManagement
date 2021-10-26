using IoT.IncidentManagement.Application.Models;

using MediatR;

namespace IoT.IncidentManagement.Application.Features.ManagerActions.Commands.Get.One
{
    public class GetManagerActionRequest : IRequest<ManagerActionDto>
    {
        public int Id { get; set; }
    }
}
