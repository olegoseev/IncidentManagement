using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Incidents.Commands.Get.Details
{
    public class GetIncidentDetailsHandler : IRequestHandler<GetIncidentDetailsRequest, IncidentDto>
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IMapper _mapper;

        public GetIncidentDetailsHandler(IIncidentRepository incidentRepository, IMapper mapper)
        {
            _incidentRepository = incidentRepository;
            _mapper = mapper;
        }

        public async Task<IncidentDto> Handle(GetIncidentDetailsRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            var incident = await _incidentRepository.GetIncidentDetailsByIdAsync(request.Id);
            _ = incident ?? throw new NotFoundException(nameof(incident), request.Id);

            return _mapper.Map<IncidentDto>(incident);
        }
    }
}
