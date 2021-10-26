using IoT.IncidentManagement.ClientDomain.Entities;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Notes.Commands.Create
{
    public class CreateNoteRequest : IRequest<Note>
    {
        public string Record { get; set; }
        public int IncidentId { get; set; }
    }
}
