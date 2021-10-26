using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Incidents.Commands.Create
{
    public class CreateIncidentHandler : IRequestHandler<CreateIncidentRequest, IncidentDto>
    {

        private readonly IIncidentRepository incidentRepository;
        private readonly IMapper mapper;

        public CreateIncidentHandler(IIncidentRepository incidentRepository, IMapper mapper)
        {
            this.incidentRepository = incidentRepository;
            this.mapper = mapper;
        }

        public async Task<IncidentDto> Handle(CreateIncidentRequest request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new BadRequestException(nameof(request));

            var validator = new CreateIncidentValidator(incidentRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if(validationResult.IsValid is false)
                throw new ValidationException(validationResult);

            var incident = mapper.Map<Incident>(request);
            var entity = await incidentRepository.AddAsync(incident);
            if(entity is null)
                throw new BadRequestException(nameof(entity));

            var details = await incidentRepository.GetIncidentDetailsByIdAsync(entity.Id);

            return mapper.Map<IncidentDto>(details);
        }
    }
}
