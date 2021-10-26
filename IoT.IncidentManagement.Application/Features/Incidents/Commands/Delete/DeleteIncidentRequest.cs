
using MediatR;

namespace IoT.IncidentManagement.Application.Features.Incidents.Commands.Delete
{
    public class DeleteIncidentRequest : IRequest
    {
        public int Id { get; set; }
    }
}
