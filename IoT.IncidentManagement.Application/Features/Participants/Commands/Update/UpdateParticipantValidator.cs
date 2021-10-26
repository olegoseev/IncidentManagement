using FluentValidation;

using IoT.IncidentManagement.Contstants;

namespace IoT.IncidentManagement.Application.Features.Participants.Commands.Update
{
    public class UpdateParticipantValidator : AbstractValidator<UpdateParticipantRequest>
    {
        public UpdateParticipantValidator()
        {
            RuleFor(x => x.Group).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(ApplicationConstants.ParticipantMaxLen)
                .WithMessage("{PropertyName} must not exceed" + $" {ApplicationConstants.ParticipantMaxLen} characters.");
            RuleFor(x => x.IncidentId).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}
