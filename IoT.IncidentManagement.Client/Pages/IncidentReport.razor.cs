using IoT.IncidentManagement.ClientApp.Features.ClosureActions.Commands.Get.One;
using IoT.IncidentManagement.ClientApp.Features.Incidents.Commands.Get;
using IoT.IncidentManagement.ClientApp.Features.Notes.Commands.Get.List;
using IoT.IncidentManagement.ClientApp.Features.Participants.Commands.Get;
using IoT.IncidentManagement.ClientDomain.Entities;

using MediatR;

using Microsoft.AspNetCore.Components;

using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Client.Pages
{
    public partial class IncidentReport
    {
        #region Services
        [Inject] private IMediator Mediator { get; set; }
        #endregion

        #region Parameters
        [Parameter] public int IncidentId { get; set; }
        #endregion


        #region Private fields
        private Incident incident;
        private Participant participant;
        private ClosureAction closureAction;
        private IEnumerable<Note> notes;
        #endregion

        protected override async Task OnInitializedAsync()
        {
            incident = await Mediator.Send(new GetIncidentDetailRequest { IncidentId = IncidentId });
            participant = await Mediator.Send(new GetParticipantsRequest { IncidentId = IncidentId });
            closureAction = (await Mediator.Send(new GetIncidentClosureActionsRequest { IncidentId = IncidentId }))
                                ?? new ClosureAction { IncidentId = IncidentId, ToDoList = "No Actions" };
            notes = await Mediator.Send(new GetIncidentNotesRequest { IncidentId = IncidentId });
        }
    }
}
