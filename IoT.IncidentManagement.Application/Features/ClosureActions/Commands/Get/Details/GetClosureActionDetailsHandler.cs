using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Get.Details
{
    public class GetClosureActionDetailsHandler : IRequestHandler<GetClosureActionDetailsRequest, ClosureActionDto>
    {
        private readonly IClosureActionRepository _repository;
        private readonly IMapper _mapper;

        public GetClosureActionDetailsHandler(IClosureActionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ClosureActionDto> Handle(GetClosureActionDetailsRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));
            var action = await _repository.GetByIdAsync(request.Id);
            _ = action ?? throw new NotFoundException(nameof(ClosureAction), request.Id);

            return _mapper.Map<ClosureActionDto>(action);
        }
    }
}
