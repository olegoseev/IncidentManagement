using MediatR;

using Microsoft.AspNetCore.Components;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace IoT.IncidentManagement.Client.Components
{
    public partial class IncidentMonitoring : IDisposable
    {
        [Inject] public IMediator Mediator { get; set; }


        private string text;
        private int interval = 30000;
        private Timer timer;
        private string cssClass = string.Empty;


        protected override Task OnInitializedAsync()
        {
            timer = new Timer(interval);
            timer.Elapsed += async (sender, e) => await HandleTimer();
            timer.Start();
            return base.OnInitializedAsync();
        }


        private Task HandleTimer()
        {
            cssClass = "item-active";
            timer?.Stop();
            return InvokeAsync(() => {
               StateHasChanged();
               });
        }


        private void ResetTimer()
        {
            cssClass = string.Empty;
            timer?.Start();
        }

        public void Dispose()
        {
            timer?.Stop();
            timer?.Dispose();
        }
    }
}
