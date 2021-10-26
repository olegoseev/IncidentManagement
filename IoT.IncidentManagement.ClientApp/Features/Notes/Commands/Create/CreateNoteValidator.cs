using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Notes.Commands.Create
{
    public class CreateNoteValidator : AbstractValidator<CreateNoteRequest>
    {
        public CreateNoteValidator()
        {
            RuleFor(x => x.IncidentId).GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(x => x.Record).NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
