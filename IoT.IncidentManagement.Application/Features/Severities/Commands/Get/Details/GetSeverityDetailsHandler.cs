using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Severities.Commands.Get.Details
{
    public class GetSeverityDetailsHandler : IRequestHandler<GetSeverityDetailsRequest, SeverityDto>
    {
        private readonly IAppRepository<Severity> _repository;
        private readonly IMapper _mapper;

        public GetSeverityDetailsHandler(IAppRepository<Severity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SeverityDto> Handle(GetSeverityDetailsRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            var severity = await _repository.GetByIdAsync(request.Id);

            _ = severity ?? throw new NotFoundException(nameof(SeverityDto), request.Id);

            return _mapper.Map<SeverityDto>(severity);
        }
    }
}
