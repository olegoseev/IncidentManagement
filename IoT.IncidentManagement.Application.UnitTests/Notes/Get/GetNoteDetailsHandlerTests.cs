using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Notes.Commands.Get.Details;
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
    public class GetNoteDetailsHandlerTests : BaseTest
    {
        [Theory]
        [InlineData(1, "Note1")]
        [InlineData(2, "Note2")]
        [InlineData(3, "Note3")]
        [InlineData(4, "Note1")]
        [InlineData(5, "Note2")]
        [InlineData(6, "Note3")]
        public async Task GetActionDetailsTest(int Id, string note)
        {
            var mockRepository = RepositoryMocks.GetNoteRepository();
            var handler = new GetNoteDetailsHandler(mockRepository, _mapper);

            var result = await handler.Handle(new GetNoteDetailsRequest { Id = Id }, CancellationToken.None);
            Assert.IsType<NoteDto>(result);
            Assert.Equal(note, result.Record);
        }


        [Fact]
        public void Handle_BadRequest()
        {
            var mockRepository = RepositoryMocks.GetNoteRepository();
            var handler = new GetNoteDetailsHandler(mockRepository, _mapper);

            Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }


        [Fact]
        public void Handle_NotFound()
        {
            var mockRepository = RepositoryMocks.GetNoteRepository();
            var handler = new GetNoteDetailsHandler(mockRepository, _mapper);

            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(new GetNoteDetailsRequest { Id = 55 }, CancellationToken.None));
        }
    }
}
