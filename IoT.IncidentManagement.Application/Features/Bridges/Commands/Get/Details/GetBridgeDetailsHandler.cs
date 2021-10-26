using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Bridges.Commands.Get.Details
{
    public class GetBridgeDetailsHandler : IRequestHandler<GetBridgeDetailsRequest, BridgeDto>
    {

        private readonly IAppRepository<Bridge> _repository;
        private readonly IMapper _mapper;

        public GetBridgeDetailsHandler(IAppRepository<Bridge> bridgeRepository, IMapper mapper)
        {
            _repository = bridgeRepository;
            _mapper = mapper;
        }

        public async Task<BridgeDto> Handle(GetBridgeDetailsRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            var bridge = await _repository.GetByIdAsync(request.Id);

            _ = bridge ?? throw new NotFoundException(nameof(Bridge), request.Id);

            return _mapper.Map<BridgeDto>(bridge);
        }
    }
}
