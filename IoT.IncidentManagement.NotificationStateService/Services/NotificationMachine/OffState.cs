using IoT.IncidentManagement.ClientApp.Features.Notifications.Get.Group;
using IoT.IncidentManagement.ClientDomain.Enum;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.NotificationStateService.Services.NotificationMachine
{
    internal class OffState : IState
    {
        private readonly StateMachine machine;
        public OffState(StateMachine machine)
        {
            this.machine = machine;
        }

        public void ProcessRequest()
        {

            machine.Notification = machine.Items
                .Where(x => x.State != NotificationState.OFF)
                .OrderBy(x => x.Order)
                .FirstOrDefault();

            if (machine.Notification is not null)
            {
                machine.Notification.InitTime = machine.StateOffTime;

                switch (machine.Notification.State)
                {
                    case NotificationState.INITIAL:
                        machine.SetState(machine.WaitingState, NotificationState.WAITING);
                        break;
                    case NotificationState.WAITING:
                        machine.SetState(machine.WaitingState, NotificationState.WAITING);
                        break;
                    case NotificationState.WARNING:
                        machine.SetState(machine.WarningState, NotificationState.WARNING);
                        break;
                    case NotificationState.ALARM:
                        machine.SetState(machine.AlarmState, NotificationState.ALARM);
                        break;
                    case NotificationState.OFF:
                    default:
                        break;
                }
            }
        }

        public override string ToString()
        {
            return "Off....";
        }
    }
}
