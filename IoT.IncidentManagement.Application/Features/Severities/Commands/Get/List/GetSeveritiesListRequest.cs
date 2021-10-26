using IoT.IncidentManagement.Application.Models;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Severities.Commands.Get.List
{
    public class GetSeveritiesListRequest : IRequest<IReadOnlyList<SeverityDto>>
    {
        
    }
}
