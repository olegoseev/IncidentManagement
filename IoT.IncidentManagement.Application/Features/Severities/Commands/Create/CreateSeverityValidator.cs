using FluentValidation;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Validators;
using IoT.IncidentManagement.Contstants;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Severities.Commands.Create
{
    public class CreateSeverityValidator : AbstractValidator<CreateSeverityRequest>
    {

        private readonly ISeverityRepository _repository;

        public CreateSeverityValidator(ISeverityRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.IncidentSeverity).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(ApplicationConstants.SeverityMaxLen)
                .WithMessage("{PropertyName} must not exceed " + $"{ApplicationConstants.SeverityMaxLen} characters.");

            RuleFor(x => x.NotificationInterval)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} cannot be negative.");

            RuleFor(x => x).MustAsync(SeverityNotExist).WithMessage("Severity already exists");
        }

        private async Task<bool> SeverityNotExist(CreateSeverityRequest request, CancellationToken cancellationToken)
        {
            return !(await _repository.SeverityExist(request.IncidentSeverity));
        }
    }
}
