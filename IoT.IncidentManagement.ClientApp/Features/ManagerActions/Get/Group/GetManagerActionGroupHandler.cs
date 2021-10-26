using AutoMapper;

using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientApp.Exceptions;
using IoT.IncidentManagement.ClientApp.Models;
using IoT.IncidentManagement.ClientDomain.Entities;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.ManagerActions.Get.Group
{
    public class GetManagerActionGroupHandler : IRequestHandler<GetManagerActionGroupRequest, IEnumerable<ManagerAction>>
    {
        private readonly IManagerActionClient client;
        private readonly IMapper mapper;

        public GetManagerActionGroupHandler(IManagerActionClient client, IMapper mapper)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public Task<IEnumerable<ManagerAction>> Handle(GetManagerActionGroupRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new BadRequestException(nameof(request));

            var dto = mapper.Map<ManagerActionDto>(request);

            return client.GetManagerActionsAsync(dto, cancellationToken);
        }
    }
}
