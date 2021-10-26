using AutoMapper;

using IoT.IncidentManagement.ClientApp.Features.ClosureActions.Commands.Create;
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
        [Inject] public IMediator MediatorService { get; set; }
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
        private IEnumerable<Status> statuses = new List<Status>();
        private EditContext editContext;
        private bool contentIsLoading = false;
        private string ClosureActions = string.Empty;
        #endregion

        private async Task HandleValidSubmit()
        {
            // update existing incident
            var request = Mapper.Map<UpdateIncidentRequest>(Incident);
            await MediatorService.Send(request);
            await MediatorService.Send(new CreateIncidentClosureActionsRequest { IncidentId = Incident.Id, ToDoList = ClosureActions });
            await OnClose.InvokeAsync();
        }

        private Task HandleInvalidSubmit()
        {
            return Task.CompletedTask;
        }
        protected override async Task OnInitializedAsync()
        {
            editContext = new EditContext(Incident);
            await LoadStatusInformationAsync();
        }

        private async Task LoadStatusInformationAsync()
        {
            statuses = await MediatorService.Send(new GetStatusListRequest());
        }
    }
}
