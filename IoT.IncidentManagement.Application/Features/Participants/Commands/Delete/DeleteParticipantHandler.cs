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

namespace IoT.IncidentManagement.Application.Features.Participants.Commands.Delete
{
    public class DeleteParticipantHandler : IRequestHandler<DeleteParticipantRequest>
    {
        private readonly IAppRepository<Participant> _repository;

        public DeleteParticipantHandler(IAppRepository<Participant> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteParticipantRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            var participant = await _repository.GetByIdAsync(request.IncidentId);

            _ = participant ?? throw new NotFoundException(nameof(Participant), request.IncidentId);

            await _repository.DeleteAsync(participant);

            return Unit.Value;
        }
    }
}
