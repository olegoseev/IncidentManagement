using FluentValidation;

using IoT.IncidentManagement.Contstants;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.ManagerActions.Events
{
    public class ManagerActionCreateEventValidator : AbstractValidator<ManagerActionCreateEvent>
    {
        public ManagerActionCreateEventValidator()
        {
            RuleFor(p => p.IncidentId).GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Order).GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Interval).GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(p => p.State).NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.InitTime).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Description).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(ApplicationConstants.ActionDescription)
                .WithMessage("{PropertyName} must not exceed " + $"{ApplicationConstants.ActionDescription} characters.");
        }
    }
}
