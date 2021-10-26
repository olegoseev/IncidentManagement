using FluentValidation;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Validators;
using IoT.IncidentManagement.Contstants;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Statuses.Commands.Create
{
    public class CreateStatusValidator : AbstractValidator<CreateStatusRequest>
    {
        private readonly IStatusRepository _statusRepository;

        public CreateStatusValidator(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;

            RuleFor(p => p.CurrentStatus)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(ApplicationConstants.StatusMaxLen)
                .WithMessage("{PropertyName} must not exceed " + $"{ApplicationConstants.StatusMaxLen} characters.");

            RuleFor(p => p)
                .MustAsync(StatusNotExist).WithMessage("Status with the same name already exits.");

        }

        private async Task<bool> StatusNotExist(CreateStatusRequest r, CancellationToken token)
        {
            return !(await _statusRepository.StatusExistsAsync(r.CurrentStatus));
        }
    }
}
