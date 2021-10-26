using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Get.List
{
    public class GetActionsListHandler : IRequestHandler<GetClosureActionsListRequest, IReadOnlyList<ClosureActionDto>>
    {
        private readonly IAppRepository<ClosureAction> _repository;
        private readonly IMapper _mapper;

        public GetActionsListHandler(IAppRepository<ClosureAction> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ClosureActionDto>> Handle(GetClosureActionsListRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            var actions = await _repository.GetAllAsync();

            _ = actions ?? throw new NotFoundException(nameof(ClosureAction));

            return _mapper.Map<IReadOnlyList<ClosureActionDto>>(actions);
        }
    }
}
