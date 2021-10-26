using FluentValidation;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Responses;
using IoT.IncidentManagement.Application.Validators;
using IoT.IncidentManagement.Contstants;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Incidents.Commands.Create
{
    public class CreateIncidentValidator : AbstractValidator<CreateIncidentRequest>
    {

        private readonly IIncidentRepository _incidentRepository;
        public CreateIncidentValidator(IIncidentRepository incidentRepository)
        {
            _incidentRepository = incidentRepository;

            RuleFor(x => x.IncidentCase)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(ApplicationConstants.IncidentCaseMaxLen)
                .WithMessage("{PropertyName} must not exceed " + $"{ApplicationConstants.IncidentCaseMaxLen} characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(ApplicationConstants.IncidentDescriptionMaxLen)
                .WithMessage("{PropertyName} must not exceed " + $"{ApplicationConstants.IncidentDescriptionMaxLen} characters.");

            RuleFor(x => x.SeverityId).NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.StatusId).NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .LessThanOrEqualTo(DateTime.UtcNow);

            RuleFor(x => x.NotifiedTime)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .LessThanOrEqualTo(DateTime.UtcNow);

            RuleFor(x => x)
                .MustAsync(IncidentCaseUnique)
                .WithMessage("Incident with the same case number already exists.");

        }

        private async Task<bool> IncidentCaseUnique(CreateIncidentRequest createIncident, CancellationToken cancellationToken)
        {
            return !(await _incidentRepository.IncidentWithCaseExistAsync(createIncident.IncidentCase));
        }

    }
}
