using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Bridges.Commands.Get.List
{
    public class GetBridgesListHandler : IRequestHandler<GetBridgesListRequest, IReadOnlyList<BridgeDto>>
    {

        private readonly IAppRepository<Bridge> _repository;
        private readonly IMapper _mapper;

        public GetBridgesListHandler(IAppRepository<Bridge> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<BridgeDto>> Handle(GetBridgesListRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            var bridges = await _repository.GetAllAsync();

            _ = bridges ?? throw new NotFoundException(nameof(Bridge));

            return _mapper.Map<List<BridgeDto>>(bridges);
        }
    }
}
