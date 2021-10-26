using AutoMapper;

using IoT.IncidentManagement.ClientApp.Features.Incidents.Commands.Get;
using IoT.IncidentManagement.ClientApp.Features.Notifications.Get.List;
using IoT.IncidentManagement.ClientApp.Features.Notifications.Update;
using IoT.IncidentManagement.ClientDomain.Entities;
using IoT.IncidentManagement.ClientDomain.Enum;
using IoT.IncidentManagement.NotificationStateService.Services.NotificationMachine;

using MediatR;

using Microsoft.AspNetCore.Components;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Client.Components
{
    public partial class IncidentNotification : IDisposable
    {
        #region Injected services
        [Inject] public IMediator Mediator { get; set; }
        [Inject] public IMapper Mapper { get; set; }
        [Inject] public IRxMachine Machine { get; set; }
        #endregion

        #region Parameters
        [Parameter] public int IncidentId { get; set; }
        #endregion

        #region Private fields
        private IEnumerable<Notification> notifications;
        private Incident incident;
        private Notification current;
        private IDisposable subscription;
        private bool showDialog = false;

        #endregion

        #region Initializers
        protected override async Task OnInitializedAsync()
        {
            await LoadNotificationsAsync();
            await LoadIncidentInformation();

            CreateSubsciption();
        }

        private void CreateSubsciption()
        {
            if (notifications is not null && incident is not null)
            {
                if (Machine.IsMachineRunning(incident.IncidentCase + NotificationGroup.EXTERNAL) ||
                    Machine.IsMachineRunning(incident.IncidentCase + NotificationGroup.INTERNAL))
                {
                    subscription = Machine.Subscribe(OnUpdateReceived);
                }
            }
        }
        #endregion

        #region Update data
        private void OnUpdateReceived(Notification item)
        {
            if (item is not null && incident is not null)
            {
                if (item.IncidentId == incident.Id)
                {
                    InvokeAsync(UpdateInformation);
                }
            }
        }

        public async Task UpdateNotificationDialog()
        {
            await LoadNotificationsAsync();
            subscription?.Dispose();
            CreateSubsciption();
            StateHasChanged();
        }
        private async Task UpdateInformation()
        {
            await LoadNotificationsAsync();

            if (notifications.Any(n => n.State != NotificationState.OFF) is false)
            {
                subscription.Dispose();
            }
            StateHasChanged();
        }
        #endregion

        #region Dialog functions
        private void OnItemClick(Notification item)
        {
            if (item is not null)
            {
                current = item;
                showDialog = true;
                StateHasChanged();
            }
        }

        public void Show()
        {
            showDialog = true;
            StateHasChanged();
        }
        public async Task DialogButtonClick(bool sent)
        {
            if (sent && current is not null)
            {
                if(current.Repeat is true)
                {
                    current.InitTime = DateTime.UtcNow;
                    current.State = NotificationState.WAITING;
                }
                else
                {
                    current.Repeat = false;
                    current.State = NotificationState.OFF;
                    current.SentTime = DateTime.UtcNow;
                }
                
                var request = Mapper.Map<UpdateNotificationRequest>(current);
                await Mediator.Send(request);
            }

            showDialog = false;
            StateHasChanged();
        }
        #endregion

        #region Loading data
        private async Task LoadNotificationsAsync()
        {
            notifications = await Mediator.Send(new GetIncidentNotificationsListRequest { IncidentId = IncidentId });
        }

        private async Task LoadIncidentInformation()
        {
            incident = await Mediator.Send(new GetIncidentDetailRequest { IncidentId = IncidentId });
        }
        #endregion

        #region Support function
        private static int RemainingTime(DateTime? timeInit, int interval)
        {
            var passed = (int)DateTime.UtcNow.Subtract(timeInit ?? DateTime.UtcNow).TotalMinutes;
            return interval - passed;
        }

        public void Dispose()
        {
            subscription?.Dispose();
        }
        #endregion
    }
}
