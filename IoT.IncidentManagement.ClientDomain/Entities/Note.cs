
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientDomain.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public DateTime RecordTime { get; set; }
        public string Record { get; set; }
        public int IncidentId { get; set; }
    }
}
