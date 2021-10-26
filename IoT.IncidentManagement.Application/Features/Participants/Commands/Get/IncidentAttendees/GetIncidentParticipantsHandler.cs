using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Participants.Commands.Get.IncidentAttendees
{
    public class GetIncidentParticipantsHandler : IRequestHandler<GetIncidentParticipantsRequest, ParticipantDto>
    {
        private readonly IParticipantRepository _repository;
        private readonly IMapper _mapper;

        public GetIncidentParticipantsHandler(IParticipantRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ParticipantDto> Handle(GetIncidentParticipantsRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            var participant = await _repository.GetByIncidentIdAsync(request.IncidentId);

            _ = participant ?? throw new NotFoundException(nameof(Participant), request.IncidentId);

            return _mapper.Map<ParticipantDto>(participant);
        }
    }
}
