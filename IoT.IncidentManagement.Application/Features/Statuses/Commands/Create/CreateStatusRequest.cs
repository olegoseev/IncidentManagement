
using IoT.IncidentManagement.Application.Models;

using MediatR;

namespace IoT.IncidentManagement.Application.Features.Statuses.Commands.Create
{
    public class CreateStatusRequest : IRequest<StatusDto>
    {
        public string CurrentStatus { get; set; }
    }
}
