using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Notifications.Commands.Delete.One
{
    public class DeleteNotificationHandler : IRequestHandler<DeleteNotificationRequest>
    {
        private readonly INotificationRepository notificationRepository;

        public DeleteNotificationHandler(INotificationRepository notificationRepository)
        {
            this.notificationRepository = notificationRepository;
        }

        public async Task<Unit> Handle(DeleteNotificationRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new BadRequestException(nameof(request));

            var notificaiton = await notificationRepository.GetByIdAsync(request.Id);
            if (notificaiton is null)
                throw new NotFoundException(nameof(notificaiton));

            await notificationRepository.DeleteAsync(notificaiton);
            return Unit.Value;
        }
    }
}
