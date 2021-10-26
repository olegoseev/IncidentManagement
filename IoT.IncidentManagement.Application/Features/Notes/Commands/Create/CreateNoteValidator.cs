using FluentValidation;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Validators;
using IoT.IncidentManagement.Contstants;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Notes.Commands.Create
{
    public class CreateNoteValidator : AbstractValidator<CreateNoteRequest>
    {

        private readonly IIncidentRepository _repository;
        public CreateNoteValidator(IIncidentRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.Record).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(ApplicationConstants.IncidentNoteMaxLen)
                .WithMessage("{PropertyName} must not exceed " + $"{ApplicationConstants.IncidentNoteMaxLen} characters.");
            RuleFor(x => x.IncidentId).NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x).MustAsync(IncidentExist).WithMessage("Incident does not exist.");
        }

        private async Task<bool> IncidentExist(CreateNoteRequest request, CancellationToken cancellationToken)
        {
            return await _repository.IncidentExistAsync(request.IncidentId);
        }
    }
}
