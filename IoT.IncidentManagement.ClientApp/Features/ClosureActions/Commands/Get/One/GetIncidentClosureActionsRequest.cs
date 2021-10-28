using IoT.IncidentManagement.ClientDomain.Entities;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.ClosureActions.Commands.Get.One
{
    public class GetIncidentClosureActionsRequest : IRequest<ClosureAction>
    {
        public int IncidentId { get; set; }
    }
}
