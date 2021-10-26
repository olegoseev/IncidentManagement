using IoT.IncidentManagement.ClientDomain.Entities;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.ClosureActions.Commands.Create
{
    public class CreateIncidentClosureActionsRequest : IRequest<ClosureAction>
    {
        public int IncidentId { get; set; }
        public string ToDoList { get; set; }
    }
}
