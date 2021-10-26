using FluentValidation;

using IoT.IncidentManagement.Contstants;

namespace IoT.IncidentManagement.Application.Features.Bridges.Commands.UpdateBridge
{
    public class UpdateBridgeValidator : AbstractValidator<UpdateBridgeRequest>
    {
        public UpdateBridgeValidator()
        {
            RuleFor(x => x.BridgeType).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(ApplicationConstants.BridgeTypeMaxLen)
                .WithMessage("{PropertyName} must not exceed" + $" {ApplicationConstants.BridgeTypeMaxLen} characters.");

            RuleFor(x => x.Id).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}
