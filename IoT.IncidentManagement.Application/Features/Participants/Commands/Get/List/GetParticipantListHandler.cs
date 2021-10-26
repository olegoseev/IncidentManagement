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

namespace IoT.IncidentManagement.Application.Features.Participants.Commands.Get.List
{
    public class GetParticipantListHandler : IRequestHandler<GetParticipantListRequest, IReadOnlyList<ParticipantDto>>
    {
        private readonly IAppRepository<Participant> _repository;
        private readonly IMapper _mapper;

        public GetParticipantListHandler(IAppRepository<Participant> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ParticipantDto>> Handle(GetParticipantListRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            var participants = await _repository.GetAllAsync();

            _ = participants ?? throw new NotFoundException(nameof(Participant));

            return _mapper.Map<IReadOnlyList<ParticipantDto>>(participants);
        }
    }
}
