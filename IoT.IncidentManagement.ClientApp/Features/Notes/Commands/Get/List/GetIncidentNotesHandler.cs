using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientApp.Exceptions;
using IoT.IncidentManagement.ClientDomain.Entities;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Notes.Commands.Get.List
{
    public class GetIncidentNotesHandler : IRequestHandler<GetIncidentNotesRequest, IEnumerable<Note>>
    {

        private readonly INoteClient client;

        public GetIncidentNotesHandler(INoteClient client)
        {
            this.client = client;
        }

        public async Task<IEnumerable<Note>> Handle(GetIncidentNotesRequest request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new BadRequestException(nameof(request));
            try
            {
                return await client.GetIncidentNotesByIncidentIdAsync(request.IncidentId, cancellationToken);
            }
            catch(ApiException ex)
            {
                return await Task.FromResult(new List<Note>());
            }
        }
    }
}
