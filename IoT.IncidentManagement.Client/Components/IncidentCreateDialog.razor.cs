using AutoMapper;

using IoT.IncidentManagement.ClientApp.Features.Bridges.Commands.Get.List;
using IoT.IncidentManagement.ClientApp.Features.Incidents.Commands.Create;
using IoT.IncidentManagement.ClientApp.Features.ManagerActions.Create.Group;
using IoT.IncidentManagement.ClientApp.Features.Notes.Commands.Create;
using IoT.IncidentManagement.ClientApp.Features.Notifications.Create;
using IoT.IncidentManagement.ClientApp.Features.Notifications.Get.Group;
using IoT.IncidentManagement.ClientApp.Features.Participants.Commands.Create;
using IoT.IncidentManagement.ClientApp.Features.Severities.Commands.Get.List;
using IoT.IncidentManagement.ClientApp.Features.Statuses.Commands.Get;
using IoT.IncidentManagement.ClientDomain.Entities;
using IoT.IncidentManagement.ClientDomain.Enum;
using IoT.IncidentManagement.NotificationStateService.Features.StateMachines.Create;

using MediatR;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Client.Components
{
    public partial class IncidentCreateDialog
    {
        #region Injected services
        [Inject] public IMapper Mapper { get; set; }
        [Inject] public IMediator Mediator { get; set; }
        #endregion

        #region Parameters
        [Parameter] public string DialogId { get; set; }
        [Parameter] public string Title { get; set; }
        [Parameter] public EventCallback OnClose { get; set; }
        #endregion

        #region Private fields
        private Incident incident = new Incident
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
        private IEnumerable<Severity> severities = new List<Severity>();
        private IEnumerable<Status> statuses = new List<Status>();
        private IEnumerable<Bridge> bridges = new List<Bridge>();
        private EditContext editContext;
        private bool addExternalNotifications = false;
        private bool addInternalNotifications = false;
        #endregion

        #region Initializers
        protected override async Task OnInitializedAsync()
        {
            editContext = new EditContext(incident);
            await LoadStatusInformationAsync();
            await LoadBridgeInformationAsync();
            await LoadSeverityInformationAsync();
        }
        #endregion


        #region submit data handlers
        private async Task HandleValidSubmit()
        {
            var createIncidentRequest = Mapper.Map<CreateIncidentRequest>(incident);
            incident = await Mediator.Send(createIncidentRequest);

            await Mediator.Send(new CreateParticipantsRequest { IncidentId = incident.Id, Group = "IoT SA" });
            await Mediator.Send(new CreateNoteRequest { IncidentId = incident.Id, Record = $"Incident {incident.IncidentCase} created" });
            await Mediator.Send(new CreateManagerActionGroupRequest { IncidentId = incident.Id });

            if (addInternalNotifications is true)
            {
                await EnableNotificationGroup(incident.Id, NotificationGroup.INTERNAL);

                var notifications = (await Mediator.Send(new GetIncidentNotificationGroupRequest
                {
                    IncidentId = incident.Id,
                    Group = NotificationGroup.INTERNAL
                })).ToList();

                await Mediator.Send(new CreateStateMachineRequest
                {
                    Incident = incident,
                    Group = NotificationGroup.INTERNAL,
                    Notifications = notifications
                });
            }


            if (addExternalNotifications is true)
            {
                await EnableNotificationGroup(incident.Id, NotificationGroup.EXTERNAL);

                var notifications = (await Mediator.Send(new GetIncidentNotificationGroupRequest
                {
                    IncidentId = incident.Id,
                    Group = NotificationGroup.EXTERNAL
                })).ToList();

                await Mediator.Send(new CreateStateMachineRequest
                {
                    Incident = incident,
                    Group = NotificationGroup.EXTERNAL,
                    Notifications = notifications
                });
            }
            await OnClose.InvokeAsync();
        }

        private Task HandleInvalidSubmit()
        {
            return Task.CompletedTask;
        }
        #endregion

        #region Notification group setup
        private Task EnableNotificationGroup(int incidentId, NotificationGroup group)
        {
            var request = new CreateNotificationGroupRequest
            {
                IncidentId = incidentId,
                Group = group,
                Interval = incident.Severity.NotificationInterval,
                InitTime = incident.StartTime
            };

            return Mediator.Send(request);
        }

        #endregion

        #region Fetch data
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