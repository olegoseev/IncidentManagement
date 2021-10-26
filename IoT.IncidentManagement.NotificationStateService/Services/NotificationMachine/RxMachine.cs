using AutoMapper;

using IoT.IncidentManagement.ClientApp.Features.Notifications.Get.One;
using IoT.IncidentManagement.ClientApp.Features.Notifications.Update;
using IoT.IncidentManagement.ClientDomain.Enum;

using MediatR;

using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using System.Timers;

namespace IoT.IncidentManagement.NotificationStateService.Services.NotificationMachine
{
    public class RxMachine : IDisposable, IRxMachine
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        public int TimeInterval => timeInterval;

        private readonly Dictionary<string, StateMachine> machines = new();
        private readonly List<IDisposable> observers = new();

        private int timeInterval;
        private readonly Timer timer;

        private readonly Subject<ClientDomain.Entities.Notification> subject = new();


        public RxMachine(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            timeInterval = 500;
            timer = new Timer(TimeInterval);
            timer.Elapsed += (sender, e) => HandleTimer();
            timer.Start();
        }

        public void Dispose()
        {
            subject?.Dispose();
            foreach (var item in observers)
                item?.Dispose();
        }

        public bool IsMachineRunning(string name)
        {
            return machines.ContainsKey(name);
        }


        public void AddMachine(StateMachine machine)
        {
            if (IsMachineRunning(machine.Name) is false)
            {
                machines.Add(machine.Name, machine);
            }
        }


        private void HandleTimer()
        {
            foreach (var machine in machines)
            {
                machine.Value.PullChain(NotifyObservers);
            }
        }

        public async Task NotifyObservers(ClientDomain.Entities.Notification item, string machineName)
        {
            if (item is not null)
            {
                var entity = await mediator.Send(new GetNotificationRequest { Id = item.Id });
                if (entity is not null)
                {
                    if (entity.State is NotificationState.OFF || entity.InitTime > item.InitTime)
                    {
                        if (machines.TryGetValue(machineName, out StateMachine machine))
                        {
                            machine.UpdateItem(entity);
                        }
                    }
                    else
                    {
                        var request = mapper.Map<UpdateNotificationRequest>(item);
                        await mediator.Send(request);
                    }
                }
                subject.OnNext(item);
            }
            else
            {
                if (IsMachineRunning(machineName))
                {
                    machines.Remove(machineName);
                }
            }
        }

        public IDisposable Subscribe(Action<ClientDomain.Entities.Notification> action)
        {
            var disposable = subject.Subscribe(action);
            observers.Add(disposable);
            return disposable;
        }

        public void RemoveMachine(string machineName)
        {
            if(IsMachineRunning(machineName) is true)
            {
                machines.Remove(machineName);
            }
        }
    }
}
