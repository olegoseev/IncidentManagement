using IoT.IncidentManagement.Application.Models;

using MediatR;

using System.Collections.Generic;

namespace IoT.IncidentManagement.Application.Features.Statuses.Commands.Get.List
{
    public class GetStatusesListRequest : IRequest<IReadOnlyList<StatusDto>>
    {

    }
}
