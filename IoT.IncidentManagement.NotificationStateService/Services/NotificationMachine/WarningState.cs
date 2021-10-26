using IoT.IncidentManagement.ClientDomain.Enum;

using System;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.NotificationStateService.Services.NotificationMachine
{
    internal class WarningState : IState
    {
        private readonly StateMachine machine;

        public WarningState(StateMachine machine)
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
                    if (machine.Notification.Interval - timePassed <= 0)
                    {
                        machine.SetState(machine.AlarmState, NotificationState.ALARM);
                    }
                    break;
            }
        }

        public override string ToString()
        {
            return "Warning....";
        }
    }
}
