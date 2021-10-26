using FluentValidation;

using IoT.IncidentManagement.Contstants;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Statuses.Commands.Update
{
    public class UpdateStatusValidator : AbstractValidator<UpdateStatusRequest>
    {
        public UpdateStatusValidator()
        {
            RuleFor(x => x.CurrentStatus).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(ApplicationConstants.StatusMaxLen)
                .WithMessage("{PropertyName} must not exceed" + $" {ApplicationConstants.StatusMaxLen} characters.");
        }
    }
}
