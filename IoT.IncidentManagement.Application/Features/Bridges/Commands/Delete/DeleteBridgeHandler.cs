using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Bridges.Commands.DeleteBridge
{
    public class DeleteBridgeHandler : IRequestHandler<DeleteBridgeRequest>
    {
        private readonly IAppRepository<Bridge> _repository;

        public DeleteBridgeHandler(IAppRepository<Bridge> bridgeRepository)
        {
            _repository = bridgeRepository;
        }

        public async Task<Unit> Handle(DeleteBridgeRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            var bridge = await _repository.GetByIdAsync(request.Id);

            _ = bridge ?? throw new NotFoundException(nameof(Bridge), request.Id);

            await _repository.DeleteAsync(bridge);

            return Unit.Value;
        }
    }
}
