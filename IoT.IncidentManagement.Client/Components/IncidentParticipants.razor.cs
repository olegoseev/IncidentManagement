using AutoMapper;

using IoT.IncidentManagement.ClientApp.Features.Participants.Commands.Get;
using IoT.IncidentManagement.ClientApp.Features.Participants.Commands.Update;
using IoT.IncidentManagement.ClientDomain.Entities;

using MediatR;

using Microsoft.AspNetCore.Components;

using System;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Client.Components
{
    public partial class IncidentParticipants
    {
        [Inject] public IMediator MediatorService { get; set; }
        [Inject] public IMapper Mapper { get; set; }
        [Parameter] public int IncidentId { get; set; }

        private Participant participants;

        protected override async Task OnInitializedAsync()
        {
            await LoadIncidentParticipantsAsync();
        }
        private async Task LoadIncidentParticipantsAsync()
        {
            participants = await MediatorService.Send(new GetParticipantsRequest { IncidentId = IncidentId });
        }

        private async Task OnDialogClose(bool isUpdated)
        {
            if(isUpdated)
            {
                var request = Mapper.Map<UpdateParticipantsRequest>(participants);
                await MediatorService.Send(request);
            }
        }

    }
}
