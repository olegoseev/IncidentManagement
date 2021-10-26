using IoT.IncidentManagement.Application.Models;

using MediatR;

using System.Collections.Generic;

namespace IoT.IncidentManagement.Application.Features.Notes.Commands.Get.List
{
    public class GetNotesListRequest : IRequest<IReadOnlyList<NoteDto>>
    {

    }
}
