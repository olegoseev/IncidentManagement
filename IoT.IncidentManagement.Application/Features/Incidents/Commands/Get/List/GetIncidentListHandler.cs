using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Incidents.Commands.Get.List
{
    public class GetIncidentListHandler : IRequestHandler<GetIncidentsListRequest, IReadOnlyList<IncidentDto>>
    {
        private readonly IIncidentRepository _repository;
        private readonly IMapper _mapper;

        public GetIncidentListHandler(IIncidentRepository incidentRepository, IMapper mapper)
        {
            _repository = incidentRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<IncidentDto>> Handle(GetIncidentsListRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));
            var incidents = await _repository.GetAllWithDetailsAsync(request.GetAll, request.Status);
            _ = incidents ?? throw new NotFoundException(nameof(Incident));

            return _mapper.Map<IReadOnlyList<IncidentDto>>(incidents);
        }
    }
}
