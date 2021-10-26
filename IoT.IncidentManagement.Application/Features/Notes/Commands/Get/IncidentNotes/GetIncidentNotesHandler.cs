using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Notes.Commands.Get.IncidentNotes
{
    public class GetIncidentNotesHandler : IRequestHandler<GetIncidentNotesRequest, IReadOnlyList<NoteDto>>
    {
        private readonly INoteRepository _repository;
        private readonly IMapper _mapper;

        public GetIncidentNotesHandler(INoteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<NoteDto>> Handle(GetIncidentNotesRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            var notes = await _repository.GetByIncidentIdAsync(request.IncidentId);

            _ = notes ?? throw new NotFoundException(nameof(Note), request.IncidentId);

            return _mapper.Map<IReadOnlyList<NoteDto>>(notes);
        }
    }
}
