using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Create
{
    public class CreateClosureActionHandler : IRequestHandler<CreateClosureActionRequest, ClosureActionDto>
    {

        private readonly IAppRepository<ClosureAction> _repository;
        private readonly IMapper _mapper;

        public CreateClosureActionHandler(IAppRepository<ClosureAction> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ClosureActionDto> Handle(CreateClosureActionRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            var validator = new CreateClosureActionValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            _ = validationResult.IsValid ? true : throw new ValidationException(validationResult);

            var action = _mapper.Map<ClosureAction>(request);
            action = await _repository.AddAsync(action);
            return _mapper.Map<ClosureActionDto>(action);
        }
    }
}
