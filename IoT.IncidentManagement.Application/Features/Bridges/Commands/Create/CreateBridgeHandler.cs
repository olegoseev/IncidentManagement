using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Bridges.Commands.Create
{
    public class CreateBridgeHandler : IRequestHandler<CreateBridgeRequest, BridgeDto>
    {

        private readonly IBridgeRepository _repository;
        private readonly IMapper _mapper;

        public CreateBridgeHandler(IBridgeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BridgeDto> Handle(CreateBridgeRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            var validator = new CreateBridgeValidator(_repository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            _ = validationResult.IsValid ? true : throw new ValidationException(validationResult);

            var bridge = _mapper.Map<Bridge>(request);
            bridge = await _repository.AddAsync(bridge);
            return _mapper.Map<BridgeDto>(bridge);
        }
    }
}
