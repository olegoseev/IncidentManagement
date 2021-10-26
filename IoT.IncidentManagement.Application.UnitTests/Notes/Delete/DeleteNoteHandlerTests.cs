using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Notes.Commands.Delete;
using IoT.IncidentManagement.Application.UnitTests.Mocks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Notes.Delete
{
    public class DeleteNoteHandlerTests : BaseTest
    {
        [Fact]
        public void Handle_BadRequest()
        {
            var mockRepository = RepositoryMocks.GetNoteRepository();
            var handler = new DeleteNoteHandler(mockRepository);

            Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }


        [Fact]
        public void Handle_NotFound()
        {
            var mockRepository = RepositoryMocks.GetNoteRepository();
            var handler = new DeleteNoteHandler(mockRepository);

            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(new DeleteNoteRequest { Id = 55 }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_DeleteNoteFromRepository()
        {
            var mockRepository = RepositoryMocks.GetNoteRepository();
            var handler = new DeleteNoteHandler(mockRepository);
            await handler.Handle(new DeleteNoteRequest { Id = 1 }, CancellationToken.None);

            var result = (await mockRepository.GetAllAsync()).ToList();

            Assert.Equal(5, result.Count);
        }
    }
}
