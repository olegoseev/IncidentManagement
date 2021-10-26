using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Notes.Commands.Update
{
    public class UpdateNoteRequest : IRequest
    {
        public int Id { get; set; }
        public string Record { get; set; }
    }
}
