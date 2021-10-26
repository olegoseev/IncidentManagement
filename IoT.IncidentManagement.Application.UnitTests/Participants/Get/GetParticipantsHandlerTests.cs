using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Participants.Commands.Get.Details;
using IoT.IncidentManagement.Application.Models;
using IoT.IncidentManagement.Application.UnitTests.Mocks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Participants.Get
{
    public class GetParticipantsHandlerTests : BaseTest
    {
        [Theory]
        [InlineData(2, "Participant2")]
        public async Task GetActionDetailsTest(int Id, string Participant)
        {
            var mockRepository = RepositoryMocks.GetParticipantRepository();
            var handler = new GetParticipantDetailsHandler(mockRepository, _mapper);

            var result = await handler.Handle(new GetParticipantDetailsRequest { Id = Id }, CancellationToken.None);
            Assert.IsType<ParticipantDto>(result);
            Assert.Equal(Participant, result.Group);
        }


        [Fact]
        public void Handle_BadRequest()
        {
            var mockRepository = RepositoryMocks.GetParticipantRepository();
            var handler = new GetParticipantDetailsHandler(mockRepository, _mapper);

            Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }


        [Fact]
        public void Handle_NotFound()
        {
            var mockRepository = RepositoryMocks.GetParticipantRepository();
            var handler = new GetParticipantDetailsHandler(mockRepository, _mapper);

            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(new GetParticipantDetailsRequest { Id = 55 }, CancellationToken.None));
        }
    }
}
