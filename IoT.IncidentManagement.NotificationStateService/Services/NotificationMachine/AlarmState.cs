using IoT.IncidentManagement.ClientDomain.Enum;

using System;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.NotificationStateService.Services.NotificationMachine
{
    internal class AlarmState : IState
    {
        private readonly StateMachine machine;

        public AlarmState(StateMachine machine)
        {
            this.machine = machine;
        }

        public void ProcessRequest()
        {
            switch (machine.Notification.State)
            {
                case NotificationState.OFF:
                    machine.StateOffTime = DateTime.UtcNow;
                    machine.Notification.SentTime = machine.StateOffTime;
                    machine.SetState(machine.OffState, NotificationState.OFF);
                    break;
                default:
                    break;
            }
        }

        public override string ToString()
        {
            return "Signaling....";
        }
    }
}
