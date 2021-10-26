using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Notes.Commands.Get.Details
{
    public class GetNoteDetailsHandler : IRequestHandler<GetNoteDetailsRequest, NoteDto>
    {
        private readonly IAppRepository<Note> _repository;
        private readonly IMapper _mapper;

        public GetNoteDetailsHandler(IAppRepository<Note> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<NoteDto> Handle(GetNoteDetailsRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            var notes = await _repository.GetByIdAsync(request.Id);

            _ = notes ?? throw new NotFoundException(nameof(Note), request.Id);

            return _mapper.Map<NoteDto>(notes);
        }
    }
}
