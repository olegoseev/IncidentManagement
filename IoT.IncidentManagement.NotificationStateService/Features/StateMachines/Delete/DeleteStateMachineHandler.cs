using IoT.IncidentManagement.NotificationStateService.Services.NotificationMachine;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.NotificationStateService.Features.StateMachines.Delete
{
    public class DeleteStateMachineHandler : IRequestHandler<DeleteStateMachineRequest>
    {

        private IRxMachine rxMachine;

        public DeleteStateMachineHandler(IRxMachine rxMachine)
        {
            this.rxMachine = rxMachine;
        }

        public async Task<Unit> Handle(DeleteStateMachineRequest request, CancellationToken cancellationToken)
        {
            if(rxMachine.IsMachineRunning(request.MachineName) is true)
            {
                rxMachine.RemoveMachine(request.MachineName);
            }
            await Task.CompletedTask;
            return Unit.Value;
        }
    }
}
