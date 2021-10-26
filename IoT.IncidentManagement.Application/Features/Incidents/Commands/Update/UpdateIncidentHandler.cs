using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Incidents.Commands.Update
{
    public class UpdateIncidentHandler : IRequestHandler<UpdateIncidentRequest>
    {
        private readonly IAppRepository<Incident> repository;

        private readonly IMapper _mapper;

        public UpdateIncidentHandler(IAppRepository<Incident> repository, IMapper mapper)
        {
            this.repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateIncidentRequest request, CancellationToken cancellationToken)
        {
            if(request == null)
                throw new BadRequestException(nameof(request));
            var incident = await repository.GetByIdAsync(request.Id);
            if(incident == null)
                throw new NotFoundException(nameof(Incident), request.Id);


            var validator = new UpdateIncidentValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if(!validationResult.IsValid)
                throw new ValidationException(validationResult);



            _mapper.Map(request, incident, typeof(UpdateIncidentRequest), typeof(Incident));
            await repository.UpdateAsync(incident);

            return Unit.Value;
        }
    }
}

