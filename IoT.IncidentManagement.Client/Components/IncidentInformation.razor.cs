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
        [Inject] private IMediator Mediator { get; set; }
        [Parameter] public EventCallback OnInformationUpdate { get; set; }

        [Parameter] public int IncidentId { get; set; }


        private Incident Incident { get; set; }
        private Task OnDialogClose()
        {
            return OnInformationUpdate.InvokeAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            Incident =  await Mediator.Send(new GetIncidentDetailRequest { IncidentId = IncidentId});
        }
    }
}
