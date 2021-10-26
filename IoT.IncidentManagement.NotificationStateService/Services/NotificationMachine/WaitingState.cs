using IoT.IncidentManagement.ClientDomain.Enum;

using System;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.NotificationStateService.Services.NotificationMachine
{
    internal class WaitingState : IState
    {
        private readonly StateMachine machine;
        public WaitingState(StateMachine machine)
        {
            this.machine = machine;
        }

        public void ProcessRequest()
        {
            var timePassed = (int)DateTime.UtcNow.Subtract(machine.Notification.InitTime ?? DateTime.UtcNow).TotalMinutes;
            switch (machine.Notification.State)
            {
                case NotificationState.OFF:
                    machine.StateOffTime = DateTime.UtcNow;
                    machine.Notification.SentTime = machine.StateOffTime;
                    machine.SetState(machine.OffState, NotificationState.OFF);
                    break;
                default:
                    if (machine.Notification.Interval - timePassed < machine.WarningTime)
                    {
                        machine.SetState(machine.WarningState, NotificationState.WARNING);
                    }
                    break;
            }
        }

        public override string ToString()
        {
            return "Waiting....";
        }
    }
}
