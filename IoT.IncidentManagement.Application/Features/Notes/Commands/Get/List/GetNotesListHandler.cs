using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Notes.Commands.Get.List
{
    public class GetNotesListHandler : IRequestHandler<GetNotesListRequest, IReadOnlyList<NoteDto>>
    {
        private readonly IAppRepository<Note> _repository;
        private readonly IMapper _mapper;

        public GetNotesListHandler(IAppRepository<Note> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<NoteDto>> Handle(GetNotesListRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            var notes = await _repository.GetAllAsync();

            _ = notes ?? throw new NotFoundException(nameof(Note));

            return _mapper.Map<IReadOnlyList<NoteDto>>(notes);
        }
    }
}
