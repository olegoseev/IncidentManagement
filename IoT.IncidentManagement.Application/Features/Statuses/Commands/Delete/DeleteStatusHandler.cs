using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Statuses.Commands.Delete
{

    public class DeleteStatusHandler : IRequestHandler<DeleteStatusRequest>
    {

        private readonly IAppRepository<Status> _repository;

        public DeleteStatusHandler(IAppRepository<Status> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteStatusRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            var status = await _repository.GetByIdAsync(request.Id);

            _ = status ?? throw new NotFoundException(nameof(Status), request.Id);

            await _repository.DeleteAsync(status);

            return Unit.Value;
        }
    }
}
