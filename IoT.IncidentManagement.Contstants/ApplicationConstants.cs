using System;

namespace IoT.IncidentManagement.Contstants
{
    public static class ApplicationConstants
    {

        public static int BridgeTypeMaxLen => 30;
        public static int ClosureActionMaxLen => 500;
        public static int IncidentCaseMaxLen => 30;
        public static int IncidentDescriptionMaxLen => 250;
        public static int ParticipantMaxLen => 250;
        public static int SeverityMaxLen => 30;
        public static int StatusMaxLen => 30;
        public static int IncidentNoteMaxLen => 500;
        public static int ActionDescription => 250;
        public static int ActionChangeInterval { get; set; } = 30;
    }
}
