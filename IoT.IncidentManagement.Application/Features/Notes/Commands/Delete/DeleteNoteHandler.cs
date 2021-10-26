using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Notes.Commands.Delete
{
    public class DeleteNoteHandler : IRequestHandler<DeleteNoteRequest>
    {

        private readonly IAppRepository<Note> _repository;

        public DeleteNoteHandler(IAppRepository<Note> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteNoteRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new BadRequestException(nameof(request));

            var note = await _repository.GetByIdAsync(request.Id);

            _ = note ?? throw new NotFoundException(nameof(Note), request.Id);

            await _repository.DeleteAsync(note);

            return Unit.Value;
        }
    }
}
