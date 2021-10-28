using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Get.Status
{
    public class GetClosureActionStatusHandler : IRequestHandler<GetClosureActionStatusRequest, bool>
    {
        private readonly IClosureActionRepository repository;

        public GetClosureActionStatusHandler(IClosureActionRepository repository)
        {
            this.repository = repository;
        }

        public Task<bool> Handle(GetClosureActionStatusRequest request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new BadRequestException(nameof(request));

            return repository.ClosureActionExists(request.IncidentId);
        }
    }
}
