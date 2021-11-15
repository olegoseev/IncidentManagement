using Microsoft.AspNetCore.Components;

namespace IoT.IncidentManagement.Client.Components;

public partial class CardForm
{
    [Parameter] public string Title { get; set; }
    [Parameter] public RenderFragment ChildContent { get; set; }
}

