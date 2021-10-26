using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Notifications.Commands.Delete.Group
{
    public class DeleteNotificationGroupHandler : IRequestHandler<DeleteNotificationGroupRequest>
    {
        private readonly INotificationRepository notificationRepository;

        public DeleteNotificationGroupHandler(INotificationRepository notificationRepository)
        {
            this.notificationRepository = notificationRepository;
        }

        public async Task<Unit> Handle(DeleteNotificationGroupRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new BadRequestException(nameof(request));

            await notificationRepository.DeleteGroupAsync(request.IncidentId, request.Group);

            return Unit.Value;
        }
    }
}
