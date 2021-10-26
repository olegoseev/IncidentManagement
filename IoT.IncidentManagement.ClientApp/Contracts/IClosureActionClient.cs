using IoT.IncidentManagement.ClientApp.Models;
using IoT.IncidentManagement.ClientDomain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Contracts
{
    public interface IClosureActionClient
    {
        public Task<ClosureAction> AddClosureActionAsync(ClosureActionDto dto, CancellationToken cancellationToken);
    }
}
