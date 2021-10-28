using AutoMapper;

using IoT.IncidentManagement.ClientApp.Features.ClosureActions.Commands.Create;
using IoT.IncidentManagement.ClientApp.Features.ClosureActions.Commands.Get;
using IoT.IncidentManagement.ClientApp.Features.ClosureActions.Commands.Update;
using IoT.IncidentManagement.ClientApp.Features.Incidents.Commands.Update;
using IoT.IncidentManagement.ClientApp.Features.Statuses.Commands.Get;
using IoT.IncidentManagement.ClientDomain.Entities;

using MediatR;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Client.Components
{
    public partial class IncidentCloseDialog
    {
        #region Inject
        [Inject] public IMapper Mapper { get; set; }
        [Inject] public IMediator Mediator { get; set; }
        #endregion

        #region Parameters
        [Parameter] public string DialogId { get; set; }
        [Parameter] public bool ShowLoading { get; set; } = false;
        [Parameter] public string Title { get; set; }
        [Parameter] public Incident Incident { get; set; }
        [Parameter] public string ButtonCaption { get; set; }
        [Parameter] public EventCallback OnClose { get; set; }
        #endregion

        #region Private fields
        private Incident incident;
        private IEnumerable<Status> statuses = new List<Status>();
        private EditContext editContext;
        private ClosureAction closureActions = new ClosureAction { ToDoList = string.Empty };
        private bool contentIsLoading = false;
        private bool updateActions = false;
        #endregion

        private async Task HandleValidSubmit()
        {
            // update existing incident
            var request = Mapper.Map<UpdateIncidentRequest>(Incident);
            await Mediator.Send(request);
            if(updateActions is true)
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
            incident.EndTime = System.DateTime.UtcNow;
            editContext = new EditContext(incident);
            await LoadStatusInformationAsync();
            await LoadClosureActionsInformation();
            updateActions = closureActions is null ? false : true;
            closureActions = new ClosureAction { ToDoList = string.Empty }; 
        }

        private async Task LoadClosureActionsInformation()
        {
            closureActions = await Mediator.Send(new GetIncidentClosureActionsRequest { IncidentId = incident.Id});
        }


        private async Task LoadStatusInformationAsync()
        {
            statuses = await Mediator.Send(new GetStatusListRequest());
        }
    }
}
