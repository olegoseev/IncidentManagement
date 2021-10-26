using IoT.IncidentManagement.Application.Models;

using MediatR;

using System.Collections.Generic;

namespace IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Get.List
{
    public class GetClosureActionsListRequest : IRequest<IReadOnlyList<ClosureActionDto>>
    {

    }
}
