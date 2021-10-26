using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Application.Responses;

namespace IoT.IncidentManagement.Application.Features.ClosureActions.Commands.Create
{
    public class CreateActionResponse : BaseResponse
    {
        public CreateActionResponse()
            : base()
        { }

        public CreateActionResponse(string message)
            : base(message)
        { }

        public CreateActionResponse(string message, bool success)
            : base(message, success)
        { }

        public ClosureActionDto ActionDto { get; set; }

    }
}
