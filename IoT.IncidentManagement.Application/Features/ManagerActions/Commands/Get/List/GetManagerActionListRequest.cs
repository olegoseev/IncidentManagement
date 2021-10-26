using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Enums;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.ManagerActions.Commands.Get.List
{
    public class GetManagerActionListRequest : IRequest<IEnumerable<ManagerActionDto>>
    {
        public int IncidentId { get; set; }
    }
}
