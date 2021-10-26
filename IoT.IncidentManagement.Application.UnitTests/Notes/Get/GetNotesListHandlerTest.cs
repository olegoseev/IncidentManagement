using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Notes.Commands.Get.List;
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
    public class GetNotesListHandlerTest : BaseTest
    {
        [Fact]
        public async Task Handle_GetNotesListTest()
        {
            var mockRepository = RepositoryMocks.GetNoteRepository();
            var handler = new GetNotesListHandler(mockRepository, _mapper);
            var result = await handler.Handle(new GetNotesListRequest(), CancellationToken.None);

            Assert.IsType<List<NoteDto>>(result);
            Assert.Equal(6, result.Count);
        }

        [Fact]
        public void Handle_BadRequest()
        {
            var mockRepository = RepositoryMocks.GetNoteRepository();
            var handler = new GetNotesListHandler(mockRepository, _mapper);

            Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }
    }
}
