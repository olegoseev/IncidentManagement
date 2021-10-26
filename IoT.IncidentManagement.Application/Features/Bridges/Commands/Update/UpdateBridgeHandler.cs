using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Bridges.Commands.UpdateBridge
{
    public class UpdateBridgeHandler : IRequestHandler<UpdateBridgeRequest>
    {

        private readonly IAppRepository<Bridge> _repository;
        private readonly IMapper _mapper;

        public UpdateBridgeHandler(IAppRepository<Bridge> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateBridgeRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            var bridge = await _repository.GetByIdAsync(request.Id);
            _ = bridge ?? throw new NotFoundException(nameof(Bridge), request.Id);

            var validator = new UpdateBridgeValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            _ = validationResult.IsValid ? true : throw new ValidationException(validationResult);



            _mapper.Map(request, bridge, typeof(UpdateBridgeRequest), typeof(Bridge));
            await _repository.UpdateAsync(bridge);

            return Unit.Value;
        }
    }
}
