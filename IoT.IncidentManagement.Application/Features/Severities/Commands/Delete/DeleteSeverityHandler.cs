using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Severities.Commands.Delete
{
    public class DeleteSeverityHandler : IRequestHandler<DeleteSeverityRequest>
    {
        private readonly IAppRepository<Severity> _repository;

        public DeleteSeverityHandler(IAppRepository<Severity> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteSeverityRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            var severity = await _repository.GetByIdAsync(request.Id);

            _ = severity ?? throw new NotFoundException(nameof(Severity), request.Id);

            await _repository.DeleteAsync(severity);

            return Unit.Value;
        }
    }
}
