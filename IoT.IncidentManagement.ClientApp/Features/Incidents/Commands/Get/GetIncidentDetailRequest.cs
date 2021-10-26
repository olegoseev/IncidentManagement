using IoT.IncidentManagement.ClientDomain.Entities;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Incidents.Commands.Get
{
    public class GetIncidentDetailRequest : IRequest<Incident>
    {
        public int IncidentId { get; set; }
    }
}
