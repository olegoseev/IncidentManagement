using IoT.IncidentManagement.Application.Models;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Notes.Commands.Get.IncidentNotes
{
    public class GetIncidentNotesRequest : IRequest<IReadOnlyList<NoteDto>>
    {
        public int IncidentId { get; set; }
    }
}
