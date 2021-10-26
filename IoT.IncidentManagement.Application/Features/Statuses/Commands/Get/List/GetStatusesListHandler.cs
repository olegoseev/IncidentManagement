using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Statuses.Commands.Get.List
{
    public class GetStatusesListHandler : IRequestHandler<GetStatusesListRequest, IReadOnlyList<StatusDto>>
    {
        private readonly IAppRepository<Status> _repository;
        private readonly IMapper _mapper;

        public GetStatusesListHandler(IAppRepository<Status> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<StatusDto>> Handle(GetStatusesListRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            var statuses = await _repository.GetAllAsync();

            _ = statuses ?? throw new NotFoundException(nameof(Status));

            return _mapper.Map<IReadOnlyList<StatusDto>>(statuses);
        }
    }
}
