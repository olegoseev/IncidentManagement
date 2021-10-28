using AutoMapper;

using IoT.IncidentManagement.ClientApp.Features.Incidents.Commands.Get;
using IoT.IncidentManagement.ClientDomain.Entities;

using MediatR;

using Microsoft.AspNetCore.Components;

using System;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Client.Components
{
    public partial class IncidentInformation
    {
        #region Services
        [Inject] private IMediator Mediator { get; set; }
        #endregion

        #region Parameters
        [Parameter] public EventCallback OnInformationUpdate { get; set; }
        [Parameter] public int IncidentId { get; set; }
        #endregion

        #region Private fields
        private Incident Incident { get; set; }
        #endregion

        private async Task OnDialogClose()
        {
            await LoadIncidentInformationAsync();
            await OnInformationUpdate.InvokeAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadIncidentInformationAsync();
        }

        private async Task LoadIncidentInformationAsync()
        {
            Incident = await Mediator.Send(new GetIncidentDetailRequest { IncidentId = IncidentId });
        }
    }
}
