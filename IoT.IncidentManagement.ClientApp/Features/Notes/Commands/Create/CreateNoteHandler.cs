using AutoMapper;

using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientApp.Exceptions;
using IoT.IncidentManagement.ClientApp.Models;
using IoT.IncidentManagement.ClientDomain.Entities;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.ClientApp.Features.Notes.Commands.Create
{
    public class CreateNoteHandler : IRequestHandler<CreateNoteRequest, Note>
    {

        private readonly INoteClient noteClient;
        private readonly IMapper mapper;

        public CreateNoteHandler(INoteClient noteClient, IMapper mapper)
        {
            this.noteClient = noteClient;
            this.mapper = mapper;
        }

        public async Task<Note> Handle(CreateNoteRequest request, CancellationToken cancellationToken)
        {
            if(request is null)
                throw new BadRequestException(nameof(request));

            var validator = new CreateNoteValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if(validationResult.IsValid is false)
                throw new ValidationException(validationResult);

            var dto = mapper.Map<NoteDto>(request);

            return await noteClient.AddNoteAsync(dto, cancellationToken);
        }
    }
}
