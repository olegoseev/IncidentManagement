using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Delete
{
    public class DeleteClosureActionHandler : IRequestHandler<DeleteClosureActionRequest>
    {

        private readonly IAppRepository<ClosureAction> repository;

        public DeleteClosureActionHandler(IAppRepository<ClosureAction> repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(DeleteClosureActionRequest request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new BadRequestException(nameof(request));

            var action = await repository.GetByIdAsync(request.Id);
            if(action is null)
                throw new NotFoundException(nameof(ClosureAction), request.Id);

            await repository.DeleteAsync(action);

            return Unit.Value;
        }
    }
}
