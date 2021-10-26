using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientApp.Exceptions;

using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Notifications.Get.EnabledTypes
{
    public class GetEnabledNotificationTypesHandler : IRequestHandler<GetEnabledNotificationTypesRequest, EnabledNotificationTypes>
    {
        private readonly INotificationClient _client;

        public GetEnabledNotificationTypesHandler(INotificationClient client)
        {
            _client = client;
        }

        public Task<EnabledNotificationTypes> Handle(GetEnabledNotificationTypesRequest request, CancellationToken cancellationToken)
        {
            if(request == null)
                throw new BadRequestException(nameof(request));

           return _client.GetEnabledNotificationTypesAsync(request.IncidentId, cancellationToken);
        }
    }
}
