﻿using AutoMapper;

using IoT.IncidentManagement.ClientApp.Features.Bridges.Commands.Get.List;
using IoT.IncidentManagement.ClientApp.Features.Incidents.Commands.Create;
using IoT.IncidentManagement.ClientApp.Features.ManagerActions.Create.Group;
using IoT.IncidentManagement.ClientApp.Features.Notes.Commands.Create;
using IoT.IncidentManagement.ClientApp.Features.Notifications.Create;
using IoT.IncidentManagement.ClientApp.Features.Notifications.Get.List;
using IoT.IncidentManagement.ClientApp.Features.Participants.Commands.Create;
using IoT.IncidentManagement.ClientApp.Features.Severities.Commands.Get.List;
using IoT.IncidentManagement.ClientApp.Features.Statuses.Commands.Get;
using IoT.IncidentManagement.ClientDomain.Entities;
using IoT.IncidentManagement.ClientDomain.Enum;
using IoT.IncidentManagement.NotificationStateService.Features.StateMachines.Create;
using IoT.IncidentManagement.NotificationStateService.Services.NotificationMachine;

using MediatR;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Client.Pages
{
    public partial class IncidentCreate
    {
        #region Inject
        [Inject] public IMapper Mapper { get; set; }
        [Inject] public IMediator Mediator { get; set; }
        [Inject] public NavigationManager NavManager { get; set; }
      //  [Inject] private IRxMachine Machine { get; set; }
        #endregion

        #region Parameters
        [Parameter] public string Title { get; set; }
        [Parameter] public string ButtonCaption { get; set; }
        [Parameter] public int Id { get; set; }
        [Parameter] public Incident Incident { get; set; }
        #endregion

        #region Private fields
        private IEnumerable<Severity> severities = new List<Severity>();
        private IEnumerable<Status> statuses = new List<Status>();
        private IEnumerable<Bridge> bridges = new List<Bridge>();
        private EditContext editContext;
        private bool addExternalNotifications = false;
        private bool addInternalNotifications = false;
        #endregion

        #region Submit data handlers
        private async Task HandleValidSubmit()
        {
            // crete new incident
            var createIncidentRequest = Mapper.Map<CreateIncidentRequest>(Incident);
            Incident = await Mediator.Send(createIncidentRequest);
            if (addExternalNotifications is true)
            {
                await EnableNotificationGroup(NotificationGroup.EXTERNAL);
            }
            if (addInternalNotifications is true)
            {
                await EnableNotificationGroup(NotificationGroup.INTERNAL);
            }
            await Mediator.Send(new CreateParticipantsRequest { IncidentId = Incident.Id, Group = "IoT SA" });
            await Mediator.Send(new CreateNoteRequest { IncidentId = Incident.Id, Record = $"Incident {Incident.IncidentCase} created" });
            await Mediator.Send(new CreateManagerActionGroupRequest { IncidentId = Incident.Id });

            var notifications = await Mediator.Send(new GetIncidentNotificationsListRequest { IncidentId = Incident.Id });

            if (addExternalNotifications)
            {
                await Mediator.Send(new CreateStateMachineRequest 
                { 
                    Incident = Incident,
                    Group = NotificationGroup.EXTERNAL,
                    Notifications = notifications.Where(i => i.Group == NotificationGroup.EXTERNAL).ToList()
                });
                //var machine = new StateMachine(Incident.IncidentCase + NotificationGroup.EXTERNAL)
                //{
                //    Items = notifications.Where(i => i.Group == NotificationGroup.EXTERNAL).ToList(),
                //    IncidentId = Incident.Id,
                //    Group = NotificationGroup.EXTERNAL
                //};
                //Machine.AddMachine(machine);
            }

            if (addInternalNotifications)
            {
                await Mediator.Send(new CreateStateMachineRequest
                {
                    Incident = Incident,
                    Group = NotificationGroup.INTERNAL,
                    Notifications = notifications.Where(i => i.Group == NotificationGroup.INTERNAL).ToList()
                });
                //var machine = new StateMachine(Incident.IncidentCase + NotificationGroup.INTERNAL)
                //{
                //    Items = notifications.Where(i => i.Group == NotificationGroup.INTERNAL).ToList(),
                //    IncidentId = Incident.Id,
                //    Group = NotificationGroup.INTERNAL
                //};
                //Machine.AddMachine(machine);
            }


            NavManager.NavigateTo($"/manage/{Incident.Id}", forceLoad: true);
        }

        private Task HandleInvalidSubmit()
        {
            return Task.CompletedTask;
        }
        #endregion

        #region Initializers
        private void InitIncident()
        {
            Incident = new Incident
            {
                Description = string.Empty,
                CustomerImpact = string.Empty,
                IncidentCase = string.Empty,
                Bridge = new Bridge { Id = 0, BridgeType = "Please select..." },
                Status = new Status { Id = 0, CurrentStatus = "Please select..." },
                Severity = new Severity { Id = 0, IncidentSeverity = "Please select...", NotificationInterval = 15 },
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow,
            };
        }
        protected override async Task OnInitializedAsync()
        {
            InitIncident();
            editContext = new EditContext(Incident);
            ButtonCaption = "Create";
            Title = "Create New Incident";
            await LoadStatusInformationAsync();
            await LoadBridgeInformationAsync();
            await LoadSeverityInformationAsync();
        }
        #endregion

        #region Notification group setup
        private Task EnableNotificationGroup(NotificationGroup group)
        {
            var request = new CreateNotificationGroupRequest
            {
                IncidentId = Incident.Id,
                Group = group,
                Interval = Incident.Severity.NotificationInterval,
                InitTime = Incident.NotifiedTime
            };

            return Mediator.Send(request);
        }
        #endregion

        #region Loading data

        private async Task LoadBridgeInformationAsync()
        {
            bridges = await Mediator.Send(new GetBridgesListRequest());
        }

        private async Task LoadStatusInformationAsync()
        {
            statuses = await Mediator.Send(new GetStatusListRequest());
        }

        private async Task LoadSeverityInformationAsync()
        {
            severities = await Mediator.Send(new GetSeverityListRequest());
        }
        #endregion

    }
}
