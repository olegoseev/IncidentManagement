using FluentValidation;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Contstants;
using IoT.IncidentManagement.Domain.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.ManagerActions.Commands.Create.One
{
    public class CreateManagerActionValidator : AbstractValidator<CreateManagerActionRequest>
    {

        public CreateManagerActionValidator()
        {
            RuleFor(p => p.IncidentId).GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Order).GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Interval).GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(p => p.State).NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.InitTime).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Description).NotNull().WithMessage("{PropertyName} is required.")
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(ApplicationConstants.ActionDescription)
                .WithMessage("{PropertyName} must not exceed " + $"{ApplicationConstants.ActionDescription} characters.");
        }
    }
}
