using IoT.IncidentManagement.ClientApp.Contracts;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.ClosureActions.Commands.Get.Status
{
    public class GetIncidentClosureActionStatusHandler : IRequestHandler<GetIncidentClosureActionStatusRequest, bool>
    {
        private readonly IClosureActionClient client;

        public GetIncidentClosureActionStatusHandler(IClosureActionClient client)
        {
            this.client = client;
        }

        public Task<bool> Handle(GetIncidentClosureActionStatusRequest request, CancellationToken cancellationToken)
        {
            return client.ClosureActionsExistAsync(request.IncidentId, cancellationToken);
        }
    }
}
