using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Incidents.Commands.Update
{
    public class UpdateIncidentValidator : AbstractValidator<UpdateIncidentRequest>
    {
        public UpdateIncidentValidator()
        {
            RuleFor(x => x.IncidentCase).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.CustomerImpact).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.StartTime).NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.NotifiedTime).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(x => x.BridgeId).GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(x => x.SeverityId).GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(x => x.StatusId).GreaterThan(0).WithMessage("{PropertyName} is required.");
        }
    }
}
