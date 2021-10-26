
using IoT.IncidentManagement.NotificationStateService.Services.NotificationMachine;

using MediatR;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.NotificationStateService.Features.StateMachines.Create
{
    public class CreateStateMachineHandler : IRequestHandler<CreateStateMachineRequest>
    {
        private readonly IRxMachine rxMachine;

        public CreateStateMachineHandler(IRxMachine rxMachine)
        {
            this.rxMachine = rxMachine;
        }



        public async Task<Unit> Handle(CreateStateMachineRequest request, CancellationToken cancellationToken)
        {
            var machine = new StateMachine(request.Incident.IncidentCase + request.Group)
            {
                Items = request.Notifications,
                IncidentId = request.Incident.Id,
                Group = request.Group
            };
            rxMachine.AddMachine(machine);
            await Task.CompletedTask;
            return Unit.Value;
        }
    }
}
