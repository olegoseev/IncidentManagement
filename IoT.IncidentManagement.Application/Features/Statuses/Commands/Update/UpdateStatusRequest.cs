using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Statuses.Commands.Update
{
    public class UpdateStatusRequest : IRequest
    {
        public int Id { get; set; }
        public string CurrentStatus { get; set; }
    }
}
