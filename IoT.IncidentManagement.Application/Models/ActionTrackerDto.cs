using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Models
{
    public class ActionTrackerDto
    {
        public int IncidentId { get; set; }
        public int StartAction { get; set; }
        public int EndAction { get; set; }
        public int CurrentAction { get; set; }
        public int Interval { get; set; }
        public int TimeRemaining { get; set; }
    }
}
