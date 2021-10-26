using IoT.IncidentManagement.Domain.Entities;

using System.Collections.Generic;

namespace IoT.IncidentManagement.Application.Models
{
    public class IncidentExtDto : IncidentDto
    {
        public string ClosureAction { get; set; }
        public string Participant { get; set; }
        public ICollection<NoteDto> Notes { get; set; }
    }
}
