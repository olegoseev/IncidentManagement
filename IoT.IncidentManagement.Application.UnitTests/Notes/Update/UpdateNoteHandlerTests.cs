using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Notes.Commands.Update;
using IoT.IncidentManagement.Application.UnitTests.Mocks;
using IoT.IncidentManagement.Contstants;

using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Notes.Update
{
    public class UpdateNoteHandlerTests : BaseTest
    {
        [Fact]
        public async Task Handle_UpdateNoteHandlerTest()
        {
            var mockRepository = RepositoryMocks.GetNoteRepository();
            var handler = new UpdateNoteHandler(mockRepository, _mapper);
            await handler.Handle(new UpdateNoteRequest
            {
                Id = 3,
                Record = "new record"
            }, CancellationToken.None);

            var note = await mockRepository.GetByIdAsync(3);

            Assert.Equal("new record", note.Record);
        }


        [Fact]
        public void Handle_BadRequest()
        {
            var mockRepository = RepositoryMocks.GetNoteRepository();
            var handler = new UpdateNoteHandler(mockRepository, _mapper);

            Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }


        [Fact]
        public void Handle_NotFound()
        {
            var mockRepository = RepositoryMocks.GetNoteRepository();
            var handler = new UpdateNoteHandler(mockRepository, _mapper);

            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(new UpdateNoteRequest { Id = 55 }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_RecordIsEmptyValidation()
        {
            var mockRepository = RepositoryMocks.GetNoteRepository();
            var handler = new UpdateNoteHandler(mockRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new UpdateNoteRequest { Id = 1 }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_RecordTooLongValidation()
        {
            var record = new string('A', ApplicationConstants.IncidentNoteMaxLen + 1);
            var mockRepository = RepositoryMocks.GetNoteRepository();
            var handler = new UpdateNoteHandler(mockRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new UpdateNoteRequest
            {
                Id = 1,
                Record = record
            }, CancellationToken.None));
        }
    }
}
