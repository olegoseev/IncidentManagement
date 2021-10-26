using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.ManagerActions.Commands.Delete
{
    public class DeleteManagerActionHandler : IRequestHandler<DeleteManagerActionRequest>
    {
        private readonly IManagerActionRepository actionRepository;

        public DeleteManagerActionHandler(IManagerActionRepository actionRepository)
        {
            this.actionRepository = actionRepository;
        }

        public async Task<Unit> Handle(DeleteManagerActionRequest request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new BadRequestException(nameof(request));

            var action = await actionRepository.GetByIdAsync(request.Id);

            if(action is null)
                throw new NotFoundException(nameof(ManagerAction), request.Id);

            await actionRepository.DeleteAsync(action);

            return Unit.Value;
        }

    }
}
