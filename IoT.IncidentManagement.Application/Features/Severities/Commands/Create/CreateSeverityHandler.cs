using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Severities.Commands.Create
{
    public class CreateSeverityHandler : IRequestHandler<CreateSeverityRequest, SeverityDto>
    {
        private readonly ISeverityRepository _repository;
        private readonly IMapper _mapper;

        public CreateSeverityHandler(ISeverityRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SeverityDto> Handle(CreateSeverityRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            var validator = new CreateSeverityValidator(_repository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            _ = validationResult.IsValid ? true : throw new ValidationException(validationResult);

            var severity = _mapper.Map<Severity>(request);
            severity = await _repository.AddAsync(severity);
            return _mapper.Map<SeverityDto>(severity);
        }
    }
}
