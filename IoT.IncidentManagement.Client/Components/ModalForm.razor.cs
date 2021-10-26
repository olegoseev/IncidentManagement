using Microsoft.AspNetCore.Components;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Client.Components
{
    public partial class ModalForm
    {
        [Parameter] public string Title { get; set; } = "Tile";
        [Parameter] public string DialogId { get; set; }

        [Parameter] public string OkBtnTitle { get; set; } = "Ok";
        [Parameter] public string CancelBtnTitle { get; set; } = "Cancel";
        [Parameter] public EventCallback<bool> OnClose {  get; set; }

        [Parameter] public RenderFragment ChildContent {  get; set; }

        [Parameter] public bool ShowOkCancel { get; set; } = false;

        private Task OnClick(bool state)
        {
            return OnClose.InvokeAsync(state);
        }


        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }
    }
}
