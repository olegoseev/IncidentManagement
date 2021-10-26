
using Microsoft.AspNetCore.Components;

using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Timers;

namespace IoT.IncidentManagement.Client.Components
{
    public partial class DigitalClock : IDisposable
    {

        [Parameter] public bool UTC { get; set; } = false;
        [Parameter] public string Title { get; set; } = "LOCAL";
        [Parameter] public string CssClass {  get; set; }

        [Parameter] public string Id { get; set; } = string.Empty;

        private readonly Timer timer = new(500);

        private DateTime Time { get; set; }

        private string hour1 = "num-0";
        private string hour2 = "num-0";
        private string minute1 = "num-0";
        private string minute2 = "num-0";
        private string second1 = "num-0";
        private string second2 = "num-0";
        private string am = "AM";

        private string hourId1 = "hour-1";
        private string hourId2 = "hour-2";
        private string minuteId1 = "minute-1";
        private string minuteId2 = "minute-2";
        private string secondId1 = "second-1";
        private string secondId2 = "second-2";
        private string amId = "ampm";


        protected override Task OnInitializedAsync()
        {
            hourId1 = "hour-1" + Id;
            hourId2 = "hour-2" + Id;
            minuteId1 = "minute-1" + Id;
            minuteId2 = "minute-2" + Id;
            secondId1 = "second-1" + Id;
            secondId2 = "second-2" + Id;
            amId = "ampm" + Id;

            timer.Elapsed += async (sender, e) => await HandleTimer();
            timer.Start();
            return base.OnInitializedAsync();
        }

        private Task HandleTimer()
        {
            return InvokeAsync(() =>
            {
                Time = UTC ? DateTime.UtcNow : DateTime.Now;

                hour1 = "num-" + Time.ToString("hh")[0];
                hour2 = "num-" + Time.ToString("hh")[1];
                minute1 = "num-" + Time.ToString("mm")[0];
                minute2 = "num-" + Time.ToString("mm")[1];
                second1 = "num-" + Time.ToString("ss")[0];
                second2 = "num-" + Time.ToString("ss")[1];

                am = Time.ToString("tt", CultureInfo.InvariantCulture);

                StateHasChanged();
            });
        }

        public void Dispose()
        {
            timer?.Stop();
            timer?.Dispose();
        }
    }
}
