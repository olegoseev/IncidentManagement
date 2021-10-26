using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Statuses.Commands.Get.Details
{
    public class GetStatusDetailsHandler : IRequestHandler<GetStatusDetailsRequest, StatusDto>
    {
        private readonly IAppRepository<Status> _repository;
        private readonly IMapper _mapper;

        public GetStatusDetailsHandler(IAppRepository<Status> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<StatusDto> Handle(GetStatusDetailsRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            var status = await _repository.GetByIdAsync(request.Id);

            _ = status ?? throw new NotFoundException(nameof(Status), request.Id);

            return _mapper.Map<StatusDto>(status);
        }
    }
}
