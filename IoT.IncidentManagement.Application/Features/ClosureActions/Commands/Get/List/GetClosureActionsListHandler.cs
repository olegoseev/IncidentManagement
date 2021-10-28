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
    public class GetClosureActionsListHandler : IRequestHandler<GetClosureActionsListRequest, IReadOnlyList<ClosureActionDto>>
    {
        private readonly IAppRepository<ClosureAction> repository;
        private readonly IMapper mapper;

        public GetClosureActionsListHandler(IAppRepository<ClosureAction> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IReadOnlyList<ClosureActionDto>> Handle(GetClosureActionsListRequest request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new BadRequestException(nameof(request));

            var actions = await repository.GetAllAsync();

            if(actions is null)
                throw new NotFoundException(nameof(ClosureAction));

            return mapper.Map<IReadOnlyList<ClosureActionDto>>(actions);
        }
    }
}
