using IoT.IncidentManagement.ClientDomain.Entities;

using System;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.NotificationStateService.Services.NotificationMachine
{
    public interface IRxMachine
    {
        public int TimeInterval { get; }
        public void AddMachine(StateMachine machine);
        public void Dispose();
        public bool IsMachineRunning(string name);
        Task NotifyObservers(Notification item, string machineName);
        public IDisposable Subscribe(Action<Notification> action);
        public void RemoveMachine(string machineName);
    }
}