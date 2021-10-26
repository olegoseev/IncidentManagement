using IoT.IncidentManagement.Domain.Entities;
using IoT.IncidentManagement.Domain.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Applications.UnitTests.Mocks
{
    public class DataMocks
    {
        public static List<Bridge> GetBridges()
        {
            return new List<Bridge>
            {
                new Bridge { Id = 1, BridgeType = "IoT Triage" },
                new Bridge { Id = 2, BridgeType = "CMD" },
                new Bridge { Id = 3, BridgeType = "NOC" }
            };
        }

        public static List<ClosureAction> GetClosureActions()
        {
            return new List<ClosureAction>
            {
                new ClosureAction { IncidentId = 1, ToDoList = "Fist action" },
                new ClosureAction { IncidentId = 2, ToDoList = "Second action" },
                new ClosureAction { IncidentId = 3, ToDoList = "Third action" },
                new ClosureAction { IncidentId = 4, ToDoList = "Forth action" },
                new ClosureAction { IncidentId = 5, ToDoList = "Fifth action" },
                new ClosureAction { IncidentId = 6, ToDoList = "Sixth action" }
            };
        }

        public static List<Severity> GetSeverities()
        {
            return new List<Severity>
            {
                new Severity { Id = 1, IncidentSeverity = "P1", NotificationInterval = 60 },
                new Severity { Id = 2, IncidentSeverity = "P2", NotificationInterval = 90 },
                new Severity { Id = 3, IncidentSeverity = "P3", NotificationInterval = 120 },
            };
        }

        public static List<Incident> GetIncidents()
        {
            return new List<Incident>
            {
                new Incident { Id = 1, Description = "Nothing Major", BridgeId = 1,
                    CreatedDate = DateTime.Now, CustomerImpact = "Not too much", IncidentCase = "INC00123456",
                    NotifiedTime = DateTime.Now, SeverityId = 1, StatusId = 2, EndTime = DateTime.Now, StartTime = DateTime.Now,
                    Bridge = new Bridge { Id = 1, BridgeType = "IoT Triage" },
                    ClosureAction = new ClosureAction { ToDoList = "Fist action" },
                    Notes = new List<Note>
                        {
                            new Note { Id = 1, Record = "Note1", IncidentId = 1 },
                            new Note { Id = 2, Record = "Note2", IncidentId = 1 },
                            new Note { Id = 3, Record = "Note3", IncidentId = 1 },

                        },
                    Participant = new Participant { IncidentId = 1, Group = "Participant1" },
                    Severity = new Severity { Id = 1, IncidentSeverity = "P1", NotificationInterval = 60 },
                    Status = new Status { Id = 2, CurrentStatus = "status2" },
                },
                new Incident { Id = 2, Description = "Nothing Major", BridgeId = 2, 
                    CreatedDate = DateTime.Now, CustomerImpact = "Not too much", IncidentCase = "INC00654321",
                    NotifiedTime = DateTime.Now, SeverityId = 2, StatusId = 3, EndTime = DateTime.Now, StartTime = DateTime.Now,
                    Bridge = new Bridge { Id = 2, BridgeType = "CMD" },
                    ClosureAction = new ClosureAction { ToDoList = "Forth action" },
                    Notes = new List<Note>
                        {
                            new Note { Id = 4, Record = "Note1", IncidentId = 2 },
                            new Note { Id = 5, Record = "Note2", IncidentId = 2 },
                            new Note { Id = 6, Record = "Note3", IncidentId = 2 }

                        },
                    Participant = new Participant { IncidentId = 2, Group = "Participant1" },
                    Severity = new Severity { Id = 3, IncidentSeverity = "P3", NotificationInterval = 120 },
                    Status = new Status { Id = 3, CurrentStatus = "status3" },
                },
                new Incident { Id = 3, Description = "Nothing Major", BridgeId = 2, 
                    CreatedDate = DateTime.Now, CustomerImpact = "Not too much", IncidentCase = "INC00654321",
                    NotifiedTime = DateTime.Now, SeverityId = 2, StatusId = 3, EndTime = DateTime.Now, StartTime = DateTime.Now,
                    Bridge = new Bridge { Id = 2, BridgeType = "CMD" },
                    ClosureAction = new ClosureAction { ToDoList = "Forth action" },
                    Notes = new List<Note>
                        {
                            new Note { Id = 4, Record = "Note1", IncidentId = 3 },
                            new Note { Id = 5, Record = "Note2", IncidentId = 3 },
                            new Note { Id = 6, Record = "Note3", IncidentId = 3 }

                        },
                   // Participant = new Participant { Id = 4, IncidentId = 2, Participator = "Participant1" },
                    Severity = new Severity { Id = 3, IncidentSeverity = "P3", NotificationInterval = 120 },
                    Status = new Status { Id = 3, CurrentStatus = "status3" },
                }
            };
        }

        public static List<Note> GetNotes()
        {
            return new List<Note>
            {
                new Note { Id = 1, Record = "Note1", IncidentId = 1 },
                new Note { Id = 2, Record = "Note2", IncidentId = 1 },
                new Note { Id = 3, Record = "Note3", IncidentId = 1 },
                new Note { Id = 4, Record = "Note1", IncidentId = 2 },
                new Note { Id = 5, Record = "Note2", IncidentId = 2 },
                new Note { Id = 6, Record = "Note3", IncidentId = 2 }
            };
        }

        public static List<Participant> GetAttendees()
        {
            return new List<Participant>
            {
                //new Participant { IncidentId = 1, Group = "Participant1" },
                new Participant { IncidentId = 2, Group = "Participant2" },

            };
        }

        public static List<Status> GetStatuses()
        {
            return new List<Status>
            {
                new Status { Id = 1, CurrentStatus = "status1" },
                new Status { Id = 2, CurrentStatus = "status2" },
                new Status { Id = 3, CurrentStatus = "status3" },
            };
        }

        public static List<Notification> GetNotifications()
        {
            return new List<Notification>
            {
                new Notification { Id = 1, Type = NotificationType.INITIAL, State = NotificationState.WAITING, Order = 1,
                    Interval = 10, IncidentId = 1, Repeat = false, InitTime = DateTime.UtcNow, Group = NotificationGroup.INTERNAL},
                new Notification { Id = 2, Type = NotificationType.FINAL, State = NotificationState.OFF, Order = 2,
                    Interval = 10, IncidentId = 2, Repeat = false, InitTime = DateTime.UtcNow, Group = NotificationGroup.INTERNAL}
            };
        }

        public int Id { get; set; }
        public int IncidentId { get; set; }
        public NotificationType NotificationType { get; set; }
        public NotificationGroup NotificationGroup { get; set; }
        public int Order { get; set; }
        public int Interval { get; set; }
        public bool Repeat { get; set; }
        public DateTime InitTime { get; set; }
        public NotificationState State { get; set; }



        public static List<ManagerAction> GetInteractions()
        {
            return new List<ManagerAction>
            {
                new ManagerAction { Id = 1,IncidentId = 1, Order = 1 , State = NotificationState.WAITING,
                   Description = "send notification"},
                new ManagerAction { Id = 2,IncidentId = 1, Order = 2 , State = NotificationState.WAITING,
                    Description = "send notification"},
                new ManagerAction { Id = 3,IncidentId = 1, Order = 3 , State = NotificationState.WAITING,
                    Description = "send notification"},
                new ManagerAction { Id = 4,IncidentId = 1, Order = 4 , State = NotificationState.WAITING,
                    Description = "send notification"},
                new ManagerAction { Id = 5,IncidentId = 1, Order = 5 , State = NotificationState.WAITING,
                     Description = "send notification"},
                new ManagerAction { Id = 6,IncidentId = 1, Order = 6 , State = NotificationState.WAITING,
                    Description = "send notification"},
            };
        }
    }
}
