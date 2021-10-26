using IoT.IncidentManagement.ClientApp.Features.Notifications.Get.Group;
using IoT.IncidentManagement.ClientDomain.Entities;
using IoT.IncidentManagement.ClientDomain.Enum;

using MediatR;

using Microsoft.AspNetCore.Components;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.NotificationStateService.Services.NotificationMachine
{
    public class StateMachine
    {
        #region Delegates
        public delegate Task StateHasChanged(Notification item, string name);
        #endregion

        #region Properties
        public string Name { get; private set; }
        public List<Notification> Items { get; set; }
        public Notification Notification { get; set; }
        public int IncidentId { get; set; }
        public NotificationGroup Group { get; set; }
        public int WarningTime { get; set; } = 10;
        public DateTime StateOffTime { get; set; } = DateTime.UtcNow;
        public IState OffState { get; }
        public IState WaitingState { get; }
        public IState WarningState { get; }
        public IState AlarmState { get; }
        #endregion


        #region Private fields
        private IState state;
        #endregion

        public StateMachine(string name)
        {
            Name = name;
            OffState = new OffState(this);
            WaitingState = new WaitingState(this);
            WarningState = new WarningState(this);
            AlarmState = new AlarmState(this);
            state = OffState;
        }

        public void PullChain(StateHasChanged action)
        {
            state.ProcessRequest();
            if (action is not null)
                action(Notification, Name);
        }

        public void UpdateItem(Notification item)
        {
            var obj = Items.FirstOrDefault(i => i.Id == item.Id);
            if (obj is not null)
            {
                obj.State = item.State;
                obj.InitTime = item.InitTime;
            }
        }

        internal void SetState(IState state, NotificationState itemState)
        {
            Notification.State = itemState;
            this.state = state;
        }

        public override string ToString()
        {
            return state.ToString();
        }
    }
}
