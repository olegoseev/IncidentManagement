
using AutoMapper;

using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientApp.Exceptions;
using IoT.IncidentManagement.ClientApp.Models;
using IoT.IncidentManagement.ClientDomain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Incidents.Commands.Create
{
    public class CreateIncidentHandler : IRequestHandler<CreateIncidentRequest, Incident>
    {
        private readonly IIncidentClient client;
        private readonly IMapper mapper;

        public CreateIncidentHandler(IIncidentClient client, IMapper mapper)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public async Task<Incident> Handle(CreateIncidentRequest request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new BadRequestException(nameof(request));

            var validator = new CreateIncidentValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if(validationResult.IsValid is false)
                throw new ValidationException(validationResult);


            var dto = mapper.Map<IncidentDto>(request);
            return await client.AddIncidentAsync(dto, cancellationToken);
        }
    }
}
