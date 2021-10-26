using IoT.IncidentManagement.ClientApp.Features.Notes.Commands.Create;
using IoT.IncidentManagement.ClientApp.Features.Notes.Commands.Get.List;
using IoT.IncidentManagement.ClientDomain.Entities;

using MediatR;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Client.Components
{
    public partial class IncidentNotes
    {
        [Inject] public IMediator Mediator { get; set; }
        [Parameter] public int IncidentId { get; set; }
       
        private IEnumerable<Note> notes;

        private string note = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await LoadNotesAsync();
        }

        private async void Submit()
        {
            note = note.Trim();
            if (!string.IsNullOrWhiteSpace(note))
            {
                await AddNoteAsync(note);
                await LoadNotesAsync();
                note = string.Empty;
                StateHasChanged();
            }
        }

        private void OnKeyDown(KeyboardEventArgs key)
        {
            if(!key.CtrlKey && key.Code == "Enter")
            {
                Submit();
            }
            if (key.CtrlKey && key.Code == "Enter")
            {
                var sb = new StringBuilder(note);
                sb.Append("\n");
                note = sb.ToString();
            }
        }


        private async Task AddNoteAsync(string note)
        {
            await Mediator.Send(new CreateNoteRequest { IncidentId = IncidentId, Record = note });
        }
        private async Task LoadNotesAsync()
        {
            notes = await Mediator.Send(new GetIncidentNotesRequest { IncidentId = IncidentId });
        }
    }
}
