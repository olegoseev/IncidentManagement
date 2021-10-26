using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Notes.Commands.Get.IncidentNotes;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Application.UnitTests.Mocks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Notes.Get
{
    public class GetIncidentNotesHandlerTests : BaseTest
    {
        [Fact]
        public async Task GetIncidentNotes()
        {
            var mockRepository = RepositoryMocks.GetNoteRepository();
            var handler = new GetIncidentNotesHandler(mockRepository, _mapper);

            var result = await handler.Handle(new GetIncidentNotesRequest { IncidentId = 1 }, CancellationToken.None);
            Assert.IsType<List<NoteDto>>(result);
            Assert.Equal(3, result.Count);

        }


        [Fact]
        public void Handle_BadRequest()
        {
            var mockRepository = RepositoryMocks.GetNoteRepository();
            var handler = new GetIncidentNotesHandler(mockRepository, _mapper);

            Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }


        [Fact]
        public void Handle_NotFound()
        {
            var mockRepository = RepositoryMocks.GetNoteRepository();
            var handler = new GetIncidentNotesHandler(mockRepository, _mapper);

            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(new GetIncidentNotesRequest { IncidentId = 55 }, CancellationToken.None));
        }
    }
}
