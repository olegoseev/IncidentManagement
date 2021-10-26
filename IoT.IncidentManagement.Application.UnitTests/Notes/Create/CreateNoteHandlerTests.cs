using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Notes.Commands.Create;
using IoT.IncidentManagement.Application.UnitTests.Mocks;
using IoT.IncidentManagement.Contstants;
using IoT.IncidentManagement.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Notes.Create
{
    public class CreateNoteHandlerTests : BaseTest
    {
        [Fact]
        public async Task Handle_AddNewNoteToRepository()
        {
            var mockRepository = RepositoryMocks.GetNoteRepository();
            var incidentRepository = RepositoryMocks.GetIncidentRepository();

            var handler = new CreateNoteHandler(mockRepository, incidentRepository, _mapper);
            await handler.Handle(new CreateNoteRequest { Record = "Twenty second", IncidentId = 1 }, CancellationToken.None);

            var notes = (await mockRepository.GetAllAsync()).ToList();
            Assert.Equal(7, notes.Count);
            Assert.Contains(notes, b => b.Record == "Twenty second");
        }

        [Fact]
        public async Task Handle_NullRequest()
        {
            var mockRepository = RepositoryMocks.GetNoteRepository();
            var incidentRepository = RepositoryMocks.GetIncidentRepository();
            var handler = new CreateNoteHandler(mockRepository, incidentRepository, _mapper);

            await Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }


        [Fact]
        public async Task Handle_RecordEmptyValidation()
        {
            var mockRepository = RepositoryMocks.GetNoteRepository();
            var incidentRepository = RepositoryMocks.GetIncidentRepository();
            var handler = new CreateNoteHandler(mockRepository, incidentRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new CreateNoteRequest(), CancellationToken.None));
        }


        [Fact]
        public async Task Handle_NoteTooLongValidation()
        {
            var record = new string('A', ApplicationConstants.IncidentNoteMaxLen + 1);

            var mockRepository = RepositoryMocks.GetNoteRepository();
            var incidentRepository = RepositoryMocks.GetIncidentRepository();
            var handler = new CreateNoteHandler(mockRepository, incidentRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new CreateNoteRequest
            {
                Record = record
            }, CancellationToken.None));
        }
    }
}
