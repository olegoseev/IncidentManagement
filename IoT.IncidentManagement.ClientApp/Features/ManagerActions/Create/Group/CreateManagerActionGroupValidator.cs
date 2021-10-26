using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.ManagerActions.Create.Group
{
    public class CreateManagerActionGroupValidator : AbstractValidator<CreateManagerActionGroupRequest>
    {
        public CreateManagerActionGroupValidator()
        {
            RuleFor(p => p.IncidentId).GreaterThan(0).WithMessage("{PropertyName} is required.");
        }
    }
}
