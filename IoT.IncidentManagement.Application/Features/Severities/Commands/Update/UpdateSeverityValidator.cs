using FluentValidation;

using IoT.IncidentManagement.Contstants;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Severities.Commands.Update
{
    public class UpdateSeverityValidator : AbstractValidator<UpdateSeverityRequest>
    {
        public UpdateSeverityValidator()
        {
            RuleFor(x => x.IncidentSeverity).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(ApplicationConstants.SeverityMaxLen)
                .WithMessage("{PropertyName} must not exceed" + $" {ApplicationConstants.SeverityMaxLen} characters.");
            RuleFor(x => x.NotificationInterval).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} cannot be negative.");
        }
    }
}
