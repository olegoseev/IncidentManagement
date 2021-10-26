using System;

namespace IoT.IncidentManagement.Application.Models
{
    public class NoteDto
    {
        public int Id { get; set; }
        public DateTime RecordTime { get; set; }
        public string Record { get; set; }
        public int IncidentId { get; set; }
    }
}
