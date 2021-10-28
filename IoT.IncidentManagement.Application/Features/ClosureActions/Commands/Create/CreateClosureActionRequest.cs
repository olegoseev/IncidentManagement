using IoT.IncidentManagement.Application.Models;

using MediatR;

namespace IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Create
{
    public class CreateClosureActionRequest : IRequest<ClosureActionDto>
    {
        public int IncidentId { get; set; }
        public string ToDoList { get; set; }
    }
}
