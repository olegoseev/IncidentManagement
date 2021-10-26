using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Notifications.Create
{
    public class CreateNotificationGroupValidator : AbstractValidator<CreateNotificationGroupRequest>
    {
        public CreateNotificationGroupValidator()
        {
            RuleFor(x => x.Group).NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.Interval).GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(x => x.IncidentId).GreaterThan(0).WithMessage("{PropertyName} is required.");
        }
    }
}
