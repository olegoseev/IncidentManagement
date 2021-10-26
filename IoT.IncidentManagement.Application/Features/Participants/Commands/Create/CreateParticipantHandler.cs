using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Participants.Commands.Create
{
    public class CreateParticipantHandler : IRequestHandler<CreateParticipantRequest, ParticipantDto>
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IIncidentRepository _incidentRepository;
        private readonly IMapper _mapper;

        public CreateParticipantHandler(IParticipantRepository participantRepository, IIncidentRepository incidentRepository, IMapper mapper)
        {
            _participantRepository = participantRepository;
            _incidentRepository = incidentRepository;
            _mapper = mapper;
        }

        public async Task<ParticipantDto> Handle(CreateParticipantRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            var validator = new CreateParticipantValidator(_participantRepository, _incidentRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            _ = validationResult.IsValid ? true : throw new ValidationException(validationResult);
 
            var participant = _mapper.Map<Participant>(request);
            var participantDto = await _participantRepository.AddAsync(participant);
            return _mapper.Map<ParticipantDto>(participantDto);
 
        }
    }
}
