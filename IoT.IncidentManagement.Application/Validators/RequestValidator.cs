using FluentValidation;

using IoT.IncidentManagement.Application.Responses;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Validators
{
    public class RequestValidator<T> : AbstractValidator<T>
    {

        private readonly BaseResponse _response;

        public RequestValidator(BaseResponse response)
        {
            _response = response;
        }

        public async Task<BaseResponse> ValidateRequestAsync(T request, CancellationToken cancellationToken)
        {
            var validationResult = await ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                _response.Success = false;
                foreach (var error in validationResult.Errors)
                {
                    _response.Errors.Add(error.ErrorMessage);
                }
            }

            return _response;
        }
    }
}
