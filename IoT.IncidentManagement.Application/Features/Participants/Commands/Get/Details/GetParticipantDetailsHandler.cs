using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Participants.Commands.Get.Details
{
    public class GetParticipantDetailsHandler : IRequestHandler<GetParticipantDetailsRequest, ParticipantDto>
    {
        private readonly IAppRepository<Participant> _repository;
        private readonly IMapper _mapper;

        public GetParticipantDetailsHandler(IAppRepository<Participant> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ParticipantDto> Handle(GetParticipantDetailsRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            var participant = await _repository.GetByIdAsync(request.Id);

            _ = participant ?? throw new NotFoundException(nameof(Participant), request.Id);

            return _mapper.Map<ParticipantDto>(participant);
        }
    }
}
