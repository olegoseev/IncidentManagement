
using IoT.IncidentManagement.Client.Components;

using Microsoft.AspNetCore.Components;

using System.Threading.Tasks;

namespace IoT.IncidentManagement.Client.Pages
{
    public partial class IncidentManage
    {
        #region Parameters
        [Parameter] public int IncidentId { get; set; }
        #endregion

        #region Private fields
        private IncidentNotification notificationDialog;
        #endregion

        private async Task InformationUpdated()
        {
            await notificationDialog.UpdateNotificationDialog();
            StateHasChanged();
        }

    }
}
