using IoT.IncidentManagement.Application.Models;

using MediatR;

using System.Collections.Generic;

namespace IoT.IncidentManagement.Application.Features.Incidents.Commands.Get.List
{
    public class GetIncidentsListRequest : IRequest<IReadOnlyList<IncidentDto>>
    {
        public bool GetAll { get; set; }
        public string Status { get; set; }
    }
}
