using FluentValidation;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Validators;
using IoT.IncidentManagement.Contstants;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Bridges.Commands.Create
{
    public class CreateBridgeValidator : AbstractValidator<CreateBridgeRequest>
    {
        private readonly IBridgeRepository _bridgeRepository;

        public CreateBridgeValidator(IBridgeRepository bridgeRepository)
        {
            _bridgeRepository = bridgeRepository;

            RuleFor(x => x.BridgeType).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(ApplicationConstants.BridgeTypeMaxLen)
                .WithMessage("{PropertyName} must not exceed " + $"{ApplicationConstants.BridgeTypeMaxLen} characters.");

            RuleFor(x => x).MustAsync(BridgeTypeNotExist).WithMessage("Troubleshooting bridge type already exists.");
        }

        private async Task<bool> BridgeTypeNotExist(CreateBridgeRequest request, CancellationToken cancellationToken)
        {
            return !(await _bridgeRepository.BridgeTypeExist(request.BridgeType));
        }
    }
}
