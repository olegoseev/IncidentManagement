using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Participants.Commands.Update
{
    public class UpdateParticipantsValidator : AbstractValidator<UpdateParticipantsRequest>
    {
        public UpdateParticipantsValidator()
        {
            RuleFor(x => x.IncidentId).GreaterThan(0).WithMessage("{PropertyName} is required");
            RuleFor(x => x.Group).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
