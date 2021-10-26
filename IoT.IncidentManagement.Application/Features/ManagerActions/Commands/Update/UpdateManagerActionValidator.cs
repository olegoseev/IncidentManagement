using FluentValidation;

using IoT.IncidentManagement.Contstants;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.ManagerActions.Commands.Update
{
    public class UpdateManagerActionValidator : AbstractValidator<UpdateManagerActionRequest>
    {
        public UpdateManagerActionValidator()
        {
            RuleFor(p => p.Id).GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(p => p.IncidentId).GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Order).GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Interval).GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(p => p.State).NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.InitTime).NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Description).NotNull().WithMessage("{PropertyName} is required.")
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(ApplicationConstants.ActionDescription)
                .WithMessage("{PropertyName} must not exceed " + $"{ApplicationConstants.ActionDescription} characters.");
        }
    }
}
