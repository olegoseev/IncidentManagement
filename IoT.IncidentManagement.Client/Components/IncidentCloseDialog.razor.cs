using AutoMapper;

using IoT.IncidentManagement.ClientApp.Features.ClosureActions.Commands.Create;
using IoT.IncidentManagement.ClientApp.Features.ClosureActions.Commands.Get.One;
using IoT.IncidentManagement.ClientApp.Features.ClosureActions.Commands.Get.Status;
using IoT.IncidentManagement.ClientApp.Features.ClosureActions.Commands.Update;
using IoT.IncidentManagement.ClientApp.Features.Incidents.Commands.Update;
using IoT.IncidentManagement.ClientApp.Features.Statuses.Commands.Get;
using IoT.IncidentManagement.ClientDomain.Entities;

using MediatR;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

using System.Linq;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Client.Components
{
    public partial class IncidentCloseDialog
    {

        private static readonly string STATUS_CLOSED = "Closed";

        #region Inject
        [Inject] public IMapper Mapper { get; set; }
        [Inject] public IMediator Mediator { get; set; }
        #endregion

        #region Parameters
        [Parameter] public string DialogId { get; set; }
        [Parameter] public string Title { get; set; }
        [Parameter] public Incident Incident { get; set; }
        [Parameter] public string ButtonCaption { get; set; }
        [Parameter] public EventCallback OnClose { get; set; }
        #endregion

        #region Private fields
        private Incident incident;
        private EditContext editContext;
        private ClosureAction closureActions = new ClosureAction { ToDoList = string.Empty };
        private bool hasActions = false;
        #endregion

        private async Task HandleValidSubmit()
        {
            // update existing incident
            var request = Mapper.Map<UpdateIncidentRequest>(Incident);
            await Mediator.Send(request);
            if (hasActions is true)
            {
                await Mediator.Send(new UpdateIncidentClosureActionsRequest { IncidentId = Incident.Id, ToDoList = closureActions.ToDoList });
            }
            else
            {
                await Mediator.Send(new CreateIncidentClosureActionsRequest { IncidentId = Incident.Id, ToDoList = closureActions.ToDoList });
            }
            await OnClose.InvokeAsync();
        }

        private Task HandleInvalidSubmit()
        {
            return Task.CompletedTask;
        }
        protected override async Task OnInitializedAsync()
        {
            incident = Mapper.Map<Incident>(Incident);
            editContext = new EditContext(incident);
            incident.EndTime = System.DateTime.UtcNow;
            incident.Status = (await Mediator.Send(new GetStatusListRequest())).FirstOrDefault(s => s.CurrentStatus == STATUS_CLOSED);

            hasActions = await Mediator.Send(new GetIncidentClosureActionStatusRequest { IncidentId = incident.Id });

            if (hasActions is true)
            {
                await LoadClosureActionsInformation();
            }
            else
            {
                closureActions = new ClosureAction { ToDoList = string.Empty };
            }
        }

        private async Task LoadClosureActionsInformation()
        {
            closureActions = await Mediator.Send(new GetIncidentClosureActionsRequest { IncidentId = incident.Id });
        }
    }
}
