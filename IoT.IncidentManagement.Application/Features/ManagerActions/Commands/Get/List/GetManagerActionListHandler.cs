using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.ManagerActions.Commands.Get.List
{
    public class GetManagerActionListHandler : IRequestHandler<GetManagerActionListRequest, IEnumerable<ManagerActionDto>>
    {
        private readonly IManagerActionRepository actionRepository;
        private readonly IMapper mapper;

        public GetManagerActionListHandler(IManagerActionRepository actionRepository, IMapper mapper)
        {
            this.actionRepository = actionRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ManagerActionDto>> Handle(GetManagerActionListRequest request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new BadRequestException(nameof(request));

            var actions = await actionRepository.GetAllAsync(request.IncidentId);

            if(actions is null)
                throw new NotFoundException(nameof(ManagerAction));

            return mapper.Map<IEnumerable<ManagerActionDto>>(actions);
        }
    }
}
