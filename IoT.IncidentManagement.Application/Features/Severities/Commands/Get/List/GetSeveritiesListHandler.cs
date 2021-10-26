using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Severities.Commands.Get.List
{
    public class GetSeveritiesListHandler : IRequestHandler<GetSeveritiesListRequest, IReadOnlyList<SeverityDto>>
    {
        private readonly IAppRepository<Severity> _repository;
        private readonly IMapper _mapper;

        public GetSeveritiesListHandler(IAppRepository<Severity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<SeverityDto>> Handle(GetSeveritiesListRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            var severities = await _repository.GetAllAsync();

            _ = severities ?? throw new NotFoundException(nameof(SeverityDto));

            return _mapper.Map<IReadOnlyList<SeverityDto>>(severities);
        }
    }
}
