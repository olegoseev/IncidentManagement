
using MediatR;

namespace IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Update
{
    public class UpdateClosureActionRequest : IRequest
    {
        public int IncidentId { get; set; }
        public string ToDoList { get; set; }
    }
}
