using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Incidents.Commands.Delete
{
    public class DeleteIncidentHandler : IRequestHandler<DeleteIncidentRequest>
    {
        private readonly IAppRepository<Incident> _incidentRepository;

        public DeleteIncidentHandler(IAppRepository<Incident> incidentRepository)
        {
            _incidentRepository = incidentRepository;
        }

        public async Task<Unit> Handle(DeleteIncidentRequest request, CancellationToken cancellationToken)
        {

            _ = request ?? throw new BadRequestException(nameof(request));

            var incident = await _incidentRepository.GetByIdAsync(request.Id);
            _ = incident ?? throw new NotFoundException(nameof(Incident), request.Id);

            await _incidentRepository.DeleteAsync(incident);

            return Unit.Value;
        }
    }
}
