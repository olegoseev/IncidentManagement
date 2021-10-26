
using IoT.IncidentManagement.Application.Features.ManagerActions.Commands.Create.Group;
using IoT.IncidentManagement.Application.Features.Notifications.Commands.Create.One;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Incidents.Events
{
    public class CreateIncidentEventHandler : INotificationHandler<CreateIncidentEvent>
    {
        private readonly IMediator _mediator;

        public CreateIncidentEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(CreateIncidentEvent createEvent, CancellationToken cancellationToken)
        {
            var incidentId = createEvent.Incident.Id;
            var notificationInterval = createEvent.Incident.Severity.NotificationInterval;

            await _mediator.Send(new CreateNotificationRequest { IncidentId = incidentId, Interval = notificationInterval }, cancellationToken);
            await _mediator.Send(new CreateManagerActionGroupRequest { IncidentId = incidentId }, cancellationToken);
        }
    }
}
