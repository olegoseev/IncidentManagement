
using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.ClosureActions.Commands.Update
{
    public class UpdateIncidentClosureActionsRequest : IRequest
    {
        public int IncidentId { get; set; }
        public string ToDoList { get; set; }
    }
}
