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
        private Note note;
        private Participant participant;
        private ClosureAction closureAction;
        #endregion

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }



    }
}
