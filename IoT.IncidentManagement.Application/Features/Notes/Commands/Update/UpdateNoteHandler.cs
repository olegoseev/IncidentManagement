using AutoMapper;

using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Domain.Entities;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Application.Features.Notes.Commands.Update
{
    public class UpdateNoteHandler : IRequestHandler<UpdateNoteRequest>
    {
        private readonly IAppRepository<Note> repository;
        private readonly IMapper mapper;

        public UpdateNoteHandler(IAppRepository<Note> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateNoteRequest request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new BadRequestException(nameof(request));

            var note = await repository.GetByIdAsync(request.Id);
            if(note is null)
                throw new NotFoundException(nameof(Note), request.Id);

            var validator = new UpdateNoteValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if(validationResult.IsValid is false)
                throw new ValidationException(validationResult);

            mapper.Map(request, note, typeof(UpdateNoteRequest), typeof(Note));
            await repository.UpdateAsync(note);

            return Unit.Value;
        }
    }
}
