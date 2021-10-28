using AutoMapper;

using IoT.IncidentManagement.ClientApp.Features.Bridges.Commands.Get.List;
using IoT.IncidentManagement.ClientApp.Features.Incidents.Commands.Update;
using IoT.IncidentManagement.ClientApp.Features.Notifications.Create;
using IoT.IncidentManagement.ClientApp.Features.Notifications.Delete;
using IoT.IncidentManagement.ClientApp.Features.Notifications.Get.EnabledTypes;
using IoT.IncidentManagement.ClientApp.Features.Notifications.Get.Group;
using IoT.IncidentManagement.ClientApp.Features.Severities.Commands.Get.List;
using IoT.IncidentManagement.ClientApp.Features.Statuses.Commands.Get;
using IoT.IncidentManagement.ClientDomain.Entities;
using IoT.IncidentManagement.ClientDomain.Enum;
using IoT.IncidentManagement.NotificationStateService.Features.StateMachines.Create;
using IoT.IncidentManagement.NotificationStateService.Features.StateMachines.Delete;

using MediatR;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Client.Components
{
    public partial class IncidentUpdateDialog
    {
        #region Inject
        [Inject] public IMapper Mapper { get; set; }
        [Inject] public IMediator Mediator { get; set; }
        #endregion

        #region Parameters
        [Parameter] public string DialogId { get; set; }
        [Parameter] public bool ShowLoading { get; set; } = false;
        [Parameter] public string Title { get; set; }
        [Parameter] public string ButtonCaption { get; set; }
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public Incident Incident { get; set; }
        #endregion

        #region Private fields
        private IEnumerable<Severity> severities = new List<Severity>();
        private IEnumerable<Status> statuses = new List<Status>();
        private IEnumerable<Bridge> bridges = new List<Bridge>();
        private EditContext editContext;
        private Incident incident;
        private EnabledNotificationTypes notificationTypes = new() { ExternalNotificationEnabled = false, InternalNotificationEnabled = false };
        private bool addExternalNotifications = false;
        private bool addInternalNotifications = false;
        private bool contentIsLoading = false;
        #endregion

        #region submit data handlers
        private async Task HandleValidSubmit()
        {
            // update existing incident
            var updateIncidentRequest = Mapper.Map<UpdateIncidentRequest>(Incident);
            await Mediator.Send(updateIncidentRequest);

            if (addInternalNotifications is true && notificationTypes.InternalNotificationEnabled is false)
            {
                notificationTypes.InternalNotificationEnabled = true;
                await EnableNotificationGroup(Incident.Id, NotificationGroup.INTERNAL);

                var notifications = (await Mediator.Send(new GetIncidentNotificationGroupRequest
                {
                    IncidentId = Incident.Id,
                    Group = NotificationGroup.INTERNAL
                })).ToList();

                await Mediator.Send(new CreateStateMachineRequest
                {
                    Incident = Incident,
                    Group = NotificationGroup.INTERNAL,
                    Notifications = notifications
                });
            }

            if (addInternalNotifications is false && notificationTypes.InternalNotificationEnabled is true)
            {
                notificationTypes.InternalNotificationEnabled = false;
                await DisableNotificationGroup(Incident.Id, NotificationGroup.INTERNAL);

                await Mediator.Send(new DeleteStateMachineRequest
                {
                    MachineName = Incident.IncidentCase + NotificationGroup.INTERNAL
                });
            }

            if (addExternalNotifications is true && notificationTypes.ExternalNotificationEnabled is false)
            {
                notificationTypes.ExternalNotificationEnabled = true;
                await EnableNotificationGroup(Incident.Id, NotificationGroup.EXTERNAL);

                var notifications = (await Mediator.Send(new GetIncidentNotificationGroupRequest
                {
                    IncidentId = Incident.Id,
                    Group = NotificationGroup.EXTERNAL
                })).ToList();

                await Mediator.Send(new CreateStateMachineRequest
                {
                    Incident = Incident,
                    Group = NotificationGroup.EXTERNAL,
                    Notifications = notifications
                });
            }

            if (addExternalNotifications is false && notificationTypes.ExternalNotificationEnabled is true)
            {
                notificationTypes.ExternalNotificationEnabled = false;
                await DisableNotificationGroup(Incident.Id, NotificationGroup.EXTERNAL);

                await Mediator.Send(new DeleteStateMachineRequest
                {
                    MachineName = Incident.IncidentCase + NotificationGroup.EXTERNAL
                });
            }
            await OnClose.InvokeAsync();
        }

        private Task HandleInvalidSubmit()
        {
            return Task.CompletedTask;
        }
        #endregion

        #region Initializers
        protected override async Task OnInitializedAsync()
        {
            incident = Mapper.Map<Incident>(Incident);
            editContext = new EditContext(incident);
            contentIsLoading = true;
            await LoadEnabledNotificationTypes();
            await LoadStatusInformationAsync();
            await LoadBridgeInformationAsync();
            await LoadSeverityInformationAsync();
            contentIsLoading = false;
        }
        #endregion

        #region Notification group setup
        private Task EnableNotificationGroup(int incidentId, NotificationGroup group)
        {
            var request = new CreateNotificationGroupRequest
            {
                IncidentId = incidentId,
                Group = group,
                Interval = Incident.Severity.NotificationInterval,
                InitTime = Incident.StartTime
            };

            return Mediator.Send(request);
        }

        private Task DisableNotificationGroup(int incidentId, NotificationGroup group)
        {
            var request = new DeleteNotificationGroupRequest
            {
                IncidentId = incidentId,
                Group = group,
            };
            return Mediator.Send(request);
        }
        #endregion

        #region Fetch data
        private async Task LoadEnabledNotificationTypes()
        {
            notificationTypes = await Mediator.Send(new GetEnabledNotificationTypesRequest { IncidentId = Incident.Id });
            addExternalNotifications = notificationTypes.ExternalNotificationEnabled;
            addInternalNotifications = notificationTypes.InternalNotificationEnabled;
        }

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
