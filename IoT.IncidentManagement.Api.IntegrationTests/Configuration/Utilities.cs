using IoT.IncidentManagement.Domain.Entities;
using IoT.IncidentManagement.Domain.Enums;
using IoT.IncidentManagement.Persistence.Context;

using System;
using System.Collections.Generic;

namespace IoT.IncidentManagement.Api.IntegrationTests.Configuration
{
    public class Utilities
    {
        public static void InitializeDbForTests(IncidentManagementDbContext context)
        {

            var bridge1 = context.Bridges.Add(new Bridge { BridgeType = "IoT Triage" });
            var bridge2 = context.Bridges.Add(new Bridge { BridgeType = "CMD" });
            var bridge3 = context.Bridges.Add(new Bridge { BridgeType = "NOC" });
            context.Bridges.Add(new Bridge { BridgeType = "OPS" });

            var severity = context.Severities.Add(new Severity { IncidentSeverity = "P1", NotificationInterval = 60 });
            context.Severities.Add(new Severity { IncidentSeverity = "P2", NotificationInterval = 90 });
            context.Severities.Add(new Severity { IncidentSeverity = "P3", NotificationInterval = 120 });

            var status = context.Statuses.Add(new Status { CurrentStatus = "status1" });
            context.Statuses.Add(new Status { CurrentStatus = "status2" });
            context.Statuses.Add(new Status { CurrentStatus = "status3" });

            context.ActionsStore.Add(new ActionStore
            {
                Id = 7,
                Order = 7,
                Description = "Action1",
                InitTime = DateTime.UtcNow,
                Interval = 15,
                Repeat = false,
                State = Domain.Enums.NotificationState.WAITING,
            });
            context.ActionsStore.Add(new ActionStore
            {
                Id = 8,
                Order = 8,
                Description = "Action2",
                InitTime = DateTime.UtcNow,
                Interval = 15,
                Repeat = false,
                State = Domain.Enums.NotificationState.WAITING,
            });
            context.ActionsStore.Add(new ActionStore
            {
                Id = 9,
                Order = 9,
                Description = "Action3",
                InitTime = DateTime.UtcNow,
                Interval = 15,
                Repeat = false,
                State = Domain.Enums.NotificationState.WAITING,
            });
            context.SaveChanges();

            context.Incidents.Add(new Incident
            {
                Id = 1,
                Description = "Nothing Major",
                CreatedDate = DateTime.Now,
                CustomerImpact = "Not too much",
                IncidentCase = "INC00123456",
                NotifiedTime = DateTime.Now,
                EndTime = DateTime.Now,
                StartTime = DateTime.Now,
                BridgeId = bridge1.Entity.Id,
                Notes = new List<Note>
                        {
                            new Note { Record = "Note1"},
                            new Note { Record = "Note2"},
                            new Note { Record = "Note3"},

                        },
                SeverityId = severity.Entity.Id,
                StatusId = status.Entity.Id,
                ClosureAction = new ClosureAction {IncidentId = 1, ToDoList = "Fist action" },
                Participant = new Participant {IncidentId = 1, Group = "Participant1" }
            });

            context.SaveChanges();


            context.Notifications.Add(new Notification
            {
                IncidentId = 1,
                Id = 1,
                Order = 1,
                InitTime = DateTime.UtcNow,
                Interval = 15,
                Repeat = false,
                State = NotificationState.WAITING,
                Group = NotificationGroup.INTERNAL,
                Type = NotificationType.INITIAL,
            });
            context.Notifications.Add(new Notification
            {
                IncidentId = 1,
                Id = 2,
                Order = 2,
                InitTime = DateTime.UtcNow,
                Interval = 15,
                Repeat = false,
                State = NotificationState.WAITING,
                Group = NotificationGroup.INTERNAL,
                Type = NotificationType.UPDATE,
            });
            context.Notifications.Add(new Notification
            {
                IncidentId = 1,
                Id = 3,
                Order = 3,
                InitTime = DateTime.UtcNow,
                Interval = 15,
                Repeat = false,
                State = NotificationState.WAITING,
                Group = NotificationGroup.INTERNAL,
                Type = NotificationType.FINAL,
            });

            context.Notifications.Add(new Notification
            {
                IncidentId = 2,
                Id = 4,
                Order = 1,
                InitTime = DateTime.UtcNow,
                Interval = 15,
                Repeat = false,
                State = NotificationState.WAITING,
                Group = NotificationGroup.INTERNAL,
                Type = NotificationType.INITIAL,
            });
            context.Notifications.Add(new Notification
            {
                IncidentId = 2,
                Id = 5,
                Order = 2,
                InitTime = DateTime.UtcNow,
                Interval = 15,
                Repeat = false,
                State = NotificationState.WAITING,
                Group = NotificationGroup.INTERNAL,
                Type = NotificationType.UPDATE,
            });
            context.Notifications.Add(new Notification
            {
                IncidentId = 2,
                Id = 6,
                Order = 3,
                InitTime = DateTime.UtcNow,
                Interval = 15,
                Repeat = false,
                State = NotificationState.WAITING,
                Group = NotificationGroup.INTERNAL,
                Type = NotificationType.FINAL,
            });


            context.SaveChanges();

            context.ManagerActions.Add(new ManagerAction
            {
                IncidentId = 1,
                Id = 9,
                Order = 9,
                Description = "Action9",
                InitTime = DateTime.UtcNow,
                Interval = 15,
                Repeat = false,
                State = Domain.Enums.NotificationState.WAITING,
            });

            context.ManagerActions.Add(new ManagerAction
            {
                IncidentId = 1,
                Id = 10,
                Order = 9,
                Description = "Action10",
                InitTime = DateTime.UtcNow,
                Interval = 15,
                Repeat = false,
                State = Domain.Enums.NotificationState.WAITING,
            });

            context.SaveChanges();

            context.Incidents.Add(new Incident
            {
                Id = 2,
                Description = "Nothing Major",
                CreatedDate = DateTime.Now,
                CustomerImpact = "Not too much",
                IncidentCase = "INC00654321",
                NotifiedTime = DateTime.Now,
                EndTime = DateTime.Now,
                StartTime = DateTime.Now,
                BridgeId = bridge2.Entity.Id,
                Notes = new List<Note>
                        {
                            new Note { Record = "Note1"},
                            new Note { Record = "Note2"},
                            new Note { Record = "Note3"},

                        },
                SeverityId = severity.Entity.Id,
                StatusId = status.Entity.Id,
                ClosureAction = new ClosureAction {IncidentId = 2, ToDoList = "Second action" },
                Participant = new Participant { IncidentId = 2, Group = "Participant2" }
            });

            //context.SaveChanges();

            context.Incidents.Add(new Incident
            {
                Id = 3,
                Description = "Nothing Major",
                CreatedDate = DateTime.Now,
                CustomerImpact = "Not too much",
                IncidentCase = "INC00654321",
                NotifiedTime = DateTime.Now,
                EndTime = DateTime.Now,
                StartTime = DateTime.Now,
                Notes = new List<Note>
                        {
                            new Note { Record = "Note1"},
                            new Note { Record = "Note2"},
                            new Note { Record = "Note3"},

                        },
                BridgeId = bridge3.Entity.Id,
                SeverityId = severity.Entity.Id,
                StatusId = status.Entity.Id,
                ClosureAction = new ClosureAction { IncidentId = 3, ToDoList = "Third action" },
                Participant = new Participant { IncidentId = 3, Group = "Participant3" }
            });

            context.Incidents.Add(new Incident
            {
                Id = 4,
                Description = "Nothing Major",
                CreatedDate = DateTime.Now,
                CustomerImpact = "Not too much",
                IncidentCase = "INC00654461",
                NotifiedTime = DateTime.Now,
                EndTime = DateTime.Now,
                StartTime = DateTime.Now,
                Notes = new List<Note>
                        {
                            new Note { Record = "Note1"},
                            new Note { Record = "Note2"},
                            new Note { Record = "Note3"},

                        },
                BridgeId = bridge3.Entity.Id,
                SeverityId = severity.Entity.Id,
                StatusId = status.Entity.Id,
                //ClosureAction = new ClosureAction { ToDoList = "Third action" },
                //Participant = new Participant { Group = "Participant3" }
            });

            context.SaveChanges();

        }
    }
}
