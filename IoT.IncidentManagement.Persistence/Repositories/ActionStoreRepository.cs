using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Domain.Entities;
using IoT.IncidentManagement.Persistence.Context;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Persistence.Repositories
{
    public class ActionStoreRepository : AppRepository<ActionStore>, IActionStoreRepository
    {
        public ActionStoreRepository(IncidentManagementDbContext dbContext) : base(dbContext)
        {
        }
    }
}
