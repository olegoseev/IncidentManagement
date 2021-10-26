using IoT.IncidentManagement.Application.Models;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.ActionStore.Get.List
{
    public class GetActionListFromStoreRequest : IRequest<IReadOnlyList<ActionStoreDto>>
    {
    }
}
