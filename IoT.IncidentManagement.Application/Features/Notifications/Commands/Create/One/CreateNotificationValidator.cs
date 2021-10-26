using FluentValidation;

using IoT.IncidentManagement.Domain.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Notifications.Commands.Create.One
{
    public class CreateNotificationValidator : AbstractValidator<CreateNotificationRequest>
    {
        public CreateNotificationValidator()
        {
            RuleFor(p => p.IncidentId).GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Group).NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Type).NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Order).GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Interval).GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(p => p.State).NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.InitTime).NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}
