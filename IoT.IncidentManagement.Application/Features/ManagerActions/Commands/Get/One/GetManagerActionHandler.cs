using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.ManagerActions.Commands.Get.One
{
    public class GetManagerActionHandler : IRequestHandler<GetManagerActionRequest, ManagerActionDto>
    {
        private readonly IManagerActionRepository actionRepository;
        private readonly IMapper mapper;

        public GetManagerActionHandler(IManagerActionRepository actionRepository, IMapper mapper)
        {
            this.actionRepository = actionRepository;
            this.mapper = mapper;
        }

        public async Task<ManagerActionDto> Handle(GetManagerActionRequest request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new BadRequestException(nameof(request));

            var action = await actionRepository.GetByIdAsync(request.Id);

            if(action is null)
                throw new NotFoundException(nameof(ManagerAction), request.Id);

            return mapper.Map<ManagerActionDto>(action);
        }
    }
}
