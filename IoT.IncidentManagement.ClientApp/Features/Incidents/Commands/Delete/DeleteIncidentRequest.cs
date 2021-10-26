using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Incidents.Commands.Delete
{
    public class DeleteIncidentRequest : IRequest
    {
        public int IncidentId { get; set; }
    }
}
