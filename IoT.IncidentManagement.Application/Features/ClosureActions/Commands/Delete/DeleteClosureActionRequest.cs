
using MediatR;

namespace IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Delete
{
    public class DeleteClosureActionRequest : IRequest
    {
        public int Id { get; set; }
    }
}
