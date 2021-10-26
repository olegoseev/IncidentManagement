namespace IoT.IncidentManagement.Application.Models
{
    public class SeverityDto
    {
        public int Id {  get; set;}
        public string IncidentSeverity { get; set; }
        public int NotificationInterval { get; set; }
    }
}
