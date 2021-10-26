using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Participants.Commands.Create
{
    public class CreateParticipantsValidator : AbstractValidator<CreateParticipantsRequest>
    {
        public CreateParticipantsValidator()
        {
            RuleFor(x => x.IncidentId).GreaterThan(0).WithMessage("{PropertyName} is required");
            RuleFor(x => x.Group).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
