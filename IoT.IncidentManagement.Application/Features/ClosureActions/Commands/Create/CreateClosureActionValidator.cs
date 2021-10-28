using FluentValidation;

using IoT.IncidentManagement.Application.Validators;
using IoT.IncidentManagement.Contstants;

namespace IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Create
{
    public class CreateClosureActionValidator : AbstractValidator<CreateClosureActionRequest>
    {
        public CreateClosureActionValidator()
        {
            RuleFor(x => x.ToDoList).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(ApplicationConstants.ClosureActionMaxLen)
                .WithMessage("{PropertyName} must not exceed " + $"{ApplicationConstants.ClosureActionMaxLen} characters.");
            RuleFor(x => x.IncidentId).GreaterThan(0).WithMessage("{PropertyName} is required.");
        }
    }
}
