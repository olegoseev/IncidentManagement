using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Get.Status
{
    public class GetClosureActionStatusRequest : IRequest<bool>
    {
        public int IncidentId { get; set; }
    }
}
