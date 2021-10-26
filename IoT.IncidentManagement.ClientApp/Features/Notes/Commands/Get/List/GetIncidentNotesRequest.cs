using IoT.IncidentManagement.ClientDomain.Entities;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Notes.Commands.Get.List
{
    public class GetIncidentNotesRequest :IRequest<IEnumerable<Note>>
    {
        public int IncidentId { get; set; }
    }
}
