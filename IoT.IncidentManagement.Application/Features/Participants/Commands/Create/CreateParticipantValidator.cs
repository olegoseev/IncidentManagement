using FluentValidation;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Validators;
using IoT.IncidentManagement.Contstants;
using IoT.IncidentManagement.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Participants.Commands.Create
{
    public class CreateParticipantValidator : AbstractValidator<CreateParticipantRequest>
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IParticipantRepository _participantRepository;
        public CreateParticipantValidator(IParticipantRepository participantRepository, IIncidentRepository incidentRepository) 
        {
            _incidentRepository = incidentRepository;
            _participantRepository = participantRepository;

            RuleFor(x => x.Group).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(ApplicationConstants.ParticipantMaxLen)
                .WithMessage("{PropertyName} must not exceed " + $"{ApplicationConstants.ParticipantMaxLen} characters.");
            RuleFor(x => x.IncidentId).NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x).MustAsync(IncidentExist).WithMessage("Incident does not exist.");
            RuleFor(x => x).MustAsync(IncidentParticipantNotExist).WithMessage("Attendee record already exist");
        }

        private Task<bool> IncidentExist(CreateParticipantRequest request, CancellationToken cancellationToken)
        {
            return  _incidentRepository.IncidentExistAsync(request.IncidentId);
        }

        private async Task<bool> IncidentParticipantNotExist(CreateParticipantRequest request, CancellationToken cancellationToken)
        {
            var result = await _participantRepository.GetByIncidentIdAsync(request.IncidentId);
            return result == null;
        }
    }
}
