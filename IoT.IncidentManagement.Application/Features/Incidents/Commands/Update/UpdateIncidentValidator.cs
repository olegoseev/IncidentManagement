using FluentValidation;

using IoT.IncidentManagement.Contstants;

using System;

namespace IoT.IncidentManagement.Application.Features.Incidents.Commands.Update
{
    public class UpdateIncidentValidator : AbstractValidator<UpdateIncidentRequest>
    {
        public UpdateIncidentValidator()
        {

            RuleFor(x => x.IncidentCase)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(ApplicationConstants.IncidentCaseMaxLen)
                .WithMessage("{PropertyName} must not exceed " + $"{ApplicationConstants.IncidentCaseMaxLen} characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(ApplicationConstants.IncidentDescriptionMaxLen)
                .WithMessage("{PropertyName} must not exceed " + $"{ApplicationConstants.IncidentDescriptionMaxLen} characters.");

            RuleFor(x => x.SeverityId).NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.StatusId).NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .LessThanOrEqualTo(DateTime.UtcNow);

            RuleFor(x => x.NotifiedTime)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .LessThanOrEqualTo(DateTime.UtcNow);

            RuleFor(x => x.Id).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}
