using IoT.IncidentManagement.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Contracts.Persistence
{
    public interface IActionStoreRepository : IAppRepository<ActionStore>
    {
    }
}
