using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Notes.Commands.Create
{
    public class CreateNoteHandler : IRequestHandler<CreateNoteRequest, NoteDto>
    {

        private readonly IAppRepository<Note> _noteRepository;
        private readonly IIncidentRepository _incidentRepository;
        private readonly IMapper _mapper;

        public CreateNoteHandler(IAppRepository<Note> noteRepository, IIncidentRepository incidentRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _incidentRepository = incidentRepository;
            _mapper = mapper;
        }

        public async Task<NoteDto> Handle(CreateNoteRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            var validator = new CreateNoteValidator(_incidentRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            _ = validationResult.IsValid ? true : throw new ValidationException(validationResult);

            var note = _mapper.Map<Note>(request);
            note = await _noteRepository.AddAsync(note);
            return _mapper.Map<NoteDto>(note);
        }
    }
}
