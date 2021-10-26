using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Applications.UnitTests.Mocks;
using IoT.IncidentManagement.Domain.Entities;
using IoT.IncidentManagement.Domain.Enums;

using Moq;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.UnitTests.Mocks
{
    public class RepositoryMocks
    {

        #region Bridge repository
        public static IBridgeRepository GetBridgeRepository()
        {
            var bridges = DataMocks.GetBridges();

            var mockRepository = new Mock<IBridgeRepository>();
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(bridges);

            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int i) => bridges.SingleOrDefault(b => b.Id == i));
            mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Bridge>())).Callback<Bridge>(b => bridges.Remove(b));
            mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Bridge>())).Callback<Bridge>(b =>
            {
                var bridge = bridges.SingleOrDefault(s => s.Id == b.Id);
                bridge.BridgeType = b.BridgeType;
            });

            mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Bridge>())).ReturnsAsync(
                (Bridge bridge) =>
                {
                    bridge.Id = 99;
                    bridges.Add(bridge);
                    return bridge;
                });

            mockRepository.Setup(repo => repo.BridgeTypeExist(It.IsAny<string>())).ReturnsAsync((string type) =>
                bridges.Any<Bridge>(b => b.BridgeType == type));

            return mockRepository.Object;
        }
        #endregion

        #region ClosureAction repository

        public static IClosureActionRepository GetClosureActionRepository()
        {
            var actions = DataMocks.GetClosureActions();

            var mockRepository = new Mock<IClosureActionRepository>();
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(actions);
            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int i) => actions.SingleOrDefault(b => b.IncidentId == i));
            mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<ClosureAction>())).Callback<ClosureAction>(b =>
            {
                actions.Remove(b);
            });
            mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<ClosureAction>())).Callback<ClosureAction>(b =>
            {
                var action = actions.SingleOrDefault(s => s.IncidentId == b.IncidentId);
                action.ToDoList = b.ToDoList;
            });
            mockRepository.Setup(repo => repo.AddAsync(It.IsAny<ClosureAction>())).ReturnsAsync(
                (ClosureAction action) =>
                {
                    action.IncidentId = 7;
                    actions.Add(action);
                    return action;
                });

            return mockRepository.Object;
        }
        #endregion
        
        #region Severity repository

        public static ISeverityRepository GetSeverityRepository()
        {
            var severities = DataMocks.GetSeverities();
            var mockRepository = new Mock<ISeverityRepository>();

            mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Severity>())).ReturnsAsync((Severity severity) => 
            {
                severity.Id = 99;
                severities.Add(severity);
                return severity;
            });
            mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Severity>())).Callback<Severity>(severity => severities.Remove(severity));
            mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Severity>())).Callback<Severity>(b =>
            {
                var severity = severities.SingleOrDefault(s => s.Id == b.Id);
                severity.IncidentSeverity = b.IncidentSeverity;
                severity.NotificationInterval = b.NotificationInterval;
            });
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(severities);
            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int i) => severities.SingleOrDefault(s => s.Id == i));
            mockRepository.Setup(repo => repo.SeverityExist(It.IsAny<string>())).ReturnsAsync((string severity) => 
            severities.Any<Severity>(s => s.IncidentSeverity == severity));

            return mockRepository.Object;
        }
        #endregion
              
        #region Incident repository
        public static IIncidentRepository GetIncidentRepository()
        {
            var incidents = DataMocks.GetIncidents();
            var mockRepository = new Mock<IIncidentRepository>();


            mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Incident>())).ReturnsAsync((Incident incident) =>
            {
                incident.Id = 99;
                incidents.Add(incident);
                return incident;
            });
            mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Incident>())).Callback<Incident>(incident => incidents.Remove(incident));
            mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Incident>())).Callback<Incident>(b =>
            {
                var incident = incidents.SingleOrDefault(s => s.Id == b.Id);
                incident.IncidentCase = b.IncidentCase;
                incident.Description = b.Description;
                incident.BridgeId = b.BridgeId;
                incident.CustomerImpact = b.CustomerImpact;
                incident.SeverityId = b.SeverityId;
            });


            mockRepository.Setup(repo => repo.IncidentExistAsync(It.IsAny<int>()))
                .ReturnsAsync((int i) => incidents.Any(x => x.Id == i));
            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int i) => incidents.SingleOrDefault(n => n.Id == i));
            mockRepository.Setup(repo => repo.GetIncidentDetailsByIdAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync((int i, bool b)  => 
                incidents.SingleOrDefault(n => n.Id == i));
            return mockRepository.Object;
        }
        #endregion

        #region Note repository
        public static INoteRepository GetNoteRepository()
        {
            var notes = DataMocks.GetNotes();
            var mockRepository = new Mock<INoteRepository>();
            mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Note>())).ReturnsAsync((Note note) => 
            {
                note.Id = 99;
                notes.Add(note);
                return note;
            });
            mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Note>())).Callback<Note>(n => 
            { 
                var note = notes.SingleOrDefault(s => s.Id == n.Id);
                note.Record = n.Record;
            });
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(notes);
            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int i) => notes.SingleOrDefault(n => n.Id == i));
            mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Note>())).Callback<Note>(n => notes.Remove(n));
            mockRepository.Setup(repo => repo.GetByIncidentIdAsync(It.IsAny<int>())).ReturnsAsync((int i) => 
                notes.FindAll(n => n.IncidentId == i));
            return mockRepository.Object;
        }
        #endregion

        #region Participant
        public static IParticipantRepository GetParticipantRepository()
        {
            var participants = DataMocks.GetAttendees();
            var mockRepository = new Mock<IParticipantRepository>();

            mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Participant>())).ReturnsAsync((Participant participant) =>
            {
                participant.IncidentId = 99;
                participants.Add(participant);
                return participant;
            });
            mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Participant>())).Callback<Participant>(p =>
            {
                var participant = participants.SingleOrDefault(s => s.IncidentId == p.IncidentId);
                participant.Group = p.Group;
            });
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(participants);
            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int i) => participants.SingleOrDefault(p => p.IncidentId == i));
            mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Participant>())).Callback<Participant>(p => participants.Remove(p));
            mockRepository.Setup(repo => repo.GetByIncidentIdAsync(It.IsAny<int>())).ReturnsAsync((int i) =>
                participants.Find(p => p.IncidentId == i));
            return mockRepository.Object;
        }
        #endregion

        #region Status

        public static IStatusRepository GetStatusRepository()
        {

            var statuses = DataMocks.GetStatuses();
            var mockRepository = new Mock<IStatusRepository>();

            mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Status>())).ReturnsAsync((Status status) =>
            {
                status.Id = 99;
                statuses.Add(status);
                return status;
            });
            mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Status>())).Callback<Status>(p =>
                {
                    var status = statuses.SingleOrDefault(s => s.Id == p.Id);
                    status.CurrentStatus = p.CurrentStatus;
                });
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(statuses);
            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int i) => statuses.SingleOrDefault(p => p.Id == i));
            mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Status>())).Callback<Status>(p => statuses.Remove(p));
            return mockRepository.Object;
        }

        #endregion

        #region Notification
        public static INotificationRepository GetNotificationRepository()
        {
            var notifications = DataMocks.GetNotifications();
            var mockRepository = new Mock<INotificationRepository>();

            mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Notification>())).ReturnsAsync((Notification Notification) =>
            {
                Notification.Id = 99;
                notifications.Add(Notification);
                return Notification;
            });
            mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Notification>())).Callback<Notification>(p =>
            {
                var Notification = notifications.SingleOrDefault(s => s.Id == p.Id);
                Notification.State = p.State;
            });
            mockRepository.Setup(repo => repo.GetIncidentNotificationsAsync(It.IsAny<int>())).ReturnsAsync((int i) =>  notifications.FindAll(n => n.IncidentId == i));
            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int i) => notifications.SingleOrDefault(p => p.Id == i));
            mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Notification>())).Callback<Notification>(p => notifications.Remove(p));
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(notifications);
            return mockRepository.Object;
        }
        #endregion

    }
}
