
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Models.Mail
{
    public class Email
    {
        public List<string> To { get; set; } = new List<string>();
        public List<string> Bcc { get; set; } = new List<string>();
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
