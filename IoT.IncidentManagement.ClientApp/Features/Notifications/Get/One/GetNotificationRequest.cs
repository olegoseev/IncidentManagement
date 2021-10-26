﻿using IoT.IncidentManagement.ClientDomain.Entities;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Notifications.Get.One
{
    public class GetNotificationRequest : IRequest<Notification>
    {
        public int Id { get; set; }
    }
}
