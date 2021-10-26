using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;

using MediatR;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.ActionStore.Get.List
{
    public class GetActionListFromStoreHandler : IRequestHandler<GetActionListFromStoreRequest, IReadOnlyList<ActionStoreDto>>
    {
        private readonly IActionStoreRepository _actionStoreRepository;
        private readonly IMapper _mapper;

        public GetActionListFromStoreHandler(IActionStoreRepository actionStoreRepository, IMapper mapper)
        {
            _actionStoreRepository = actionStoreRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ActionStoreDto>> Handle(GetActionListFromStoreRequest request, CancellationToken cancellationToken)
        {
            var actions = await _actionStoreRepository.GetAllAsync();
            _ = actions ?? throw new NotFoundException(nameof(actions));

            return _mapper.Map<IReadOnlyList<ActionStoreDto>>(actions);
        }
    }
}
