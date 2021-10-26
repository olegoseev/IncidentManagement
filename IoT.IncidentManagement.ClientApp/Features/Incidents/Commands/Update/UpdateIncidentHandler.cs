using AutoMapper;

using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientApp.Exceptions;
using IoT.IncidentManagement.ClientApp.Models;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Incidents.Commands.Update
{
    public class UpdateIncidentHandler : IRequestHandler<UpdateIncidentRequest>
    {
        private readonly IIncidentClient client;
        private readonly IMapper mapper;

        public UpdateIncidentHandler(IIncidentClient client, IMapper mapper)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateIncidentRequest request, CancellationToken cancellationToken)
        {
            if(request == null)
                throw new BadRequestException(nameof(request));

            var validator = new UpdateIncidentValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if(!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var dto = mapper.Map<IncidentDto>(request);

            await client.UpdateIncidentAsync(dto, cancellationToken);
            return Unit.Value;
        }
    }
}
