using IoT.IncidentManagement.ClientApp.Features.ClosureActions.Commands.Get.One;
using IoT.IncidentManagement.ClientApp.Features.Incidents.Commands.Get;
using IoT.IncidentManagement.ClientApp.Features.Notes.Commands.Get.List;
using IoT.IncidentManagement.ClientApp.Features.Participants.Commands.Get;
using IoT.IncidentManagement.ClientDomain.Entities;

using MediatR;

using Microsoft.AspNetCore.Components;

using System;
using System.Collections.Generic;
using System.Linq;
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
        private string incidentNotes;
        private Participant participant;
        private ClosureAction closureAction;
        #endregion

        protected override async Task OnInitializedAsync()
        {
            incident = await Mediator.Send(new GetIncidentDetailRequest { IncidentId = IncidentId });
            participant = await Mediator.Send(new GetParticipantsRequest { IncidentId = IncidentId });
            closureAction = (await Mediator.Send(new GetIncidentClosureActionsRequest { IncidentId = IncidentId }))
                                ?? new ClosureAction { IncidentId = IncidentId, ToDoList = "No Actions" };
            var notes = await Mediator.Send(new GetIncidentNotesRequest { IncidentId = IncidentId });

            var strBilder = new StringBuilder();
            foreach (var note in notes)
            {
                strBilder.Append($"{note.RecordTime} {note.Record}");
            }
            incidentNotes = strBilder.ToString();

        }



    }
}
