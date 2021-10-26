
using MediatR;

namespace IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Update
{
    public class UpdateClosureActionRequest : IRequest
    {
        public int Id { get; set; }
        public string ToDoList { get; set; }
    }
}
