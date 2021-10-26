using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Statuses.Commands.Create
{
    public class CreateStatusHandler : IRequestHandler<CreateStatusRequest, StatusDto>
    {

        private readonly IStatusRepository _repository;
        private readonly IMapper _mapper;

        public CreateStatusHandler(IStatusRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<StatusDto> Handle(CreateStatusRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            var validator = new CreateStatusValidator(_repository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            _ = validationResult.IsValid ? true : throw new ValidationException(validationResult);

            var status = _mapper.Map<Status>(request);
            status = await _repository.AddAsync(status);
            return _mapper.Map<StatusDto>(status);
        }
    }
}
