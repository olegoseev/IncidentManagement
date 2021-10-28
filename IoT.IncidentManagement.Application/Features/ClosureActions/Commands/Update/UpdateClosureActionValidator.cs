using FluentValidation;

using IoT.IncidentManagement.Contstants;

namespace IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Update
{
    public class UpdateClosureActionValidator : AbstractValidator<UpdateClosureActionRequest>
    {
        public UpdateClosureActionValidator()
        {
            RuleFor(x => x.ToDoList).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(ApplicationConstants.ClosureActionMaxLen)
                .WithMessage("{PropertyName} must not exceed" + $" {ApplicationConstants.ClosureActionMaxLen} characters.");
            RuleFor(x => x.IncidentId).GreaterThan(0).WithMessage("{PropertyName} is required.");
        }
    }
}
