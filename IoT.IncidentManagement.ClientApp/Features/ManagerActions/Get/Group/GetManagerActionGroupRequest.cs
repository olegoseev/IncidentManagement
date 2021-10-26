﻿using IoT.IncidentManagement.ClientDomain.Entities;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.ManagerActions.Get.Group
{
    public class GetManagerActionGroupRequest : IRequest<IEnumerable<ManagerAction>>
    {
        public int IncidentId { get; set; }
    }
}
