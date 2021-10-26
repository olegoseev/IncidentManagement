using FluentValidation;

using IoT.IncidentManagement.Contstants;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Notes.Commands.Update
{
    public class UpdateNoteValidator : AbstractValidator<UpdateNoteRequest>
    {
        public UpdateNoteValidator()
        {
            RuleFor(x => x.Record).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(ApplicationConstants.IncidentNoteMaxLen)
                .WithMessage("{PropertyName} must not exceed" + $" {ApplicationConstants.IncidentNoteMaxLen} characters.");
        }
    }
}
