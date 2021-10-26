using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Participants.Commands.Update;
using IoT.IncidentManagement.Application.UnitTests.Mocks;
using IoT.IncidentManagement.Contstants;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Participants.Update
{
    public class UpdateParticipantHandlerTests : BaseTest
    {
        [Fact]
        public async Task Handle_UpdateParticipantHandlerTest()
        {
            var mockRepository = RepositoryMocks.GetParticipantRepository();
            var handler = new UpdateParticipantHandler(mockRepository, _mapper);
            await handler.Handle(new UpdateParticipantRequest
            {
                IncidentId = 2,
                Group = "new group"
            }, CancellationToken.None);

            var Participant = await mockRepository.GetByIdAsync(2);

            Assert.Equal("new group", Participant.Group);
        }


        [Fact]
        public void Handle_BadRequest()
        {
            var mockRepository = RepositoryMocks.GetParticipantRepository();
            var handler = new UpdateParticipantHandler(mockRepository, _mapper);

            Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }


        [Fact]
        public void Handle_NotFound()
        {
            var mockRepository = RepositoryMocks.GetParticipantRepository();
            var handler = new UpdateParticipantHandler(mockRepository, _mapper);

            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(new UpdateParticipantRequest { IncidentId = 55 }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_GroupIsEmptyValidation()
        {
            var mockRepository = RepositoryMocks.GetParticipantRepository();
            var handler = new UpdateParticipantHandler(mockRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new UpdateParticipantRequest { IncidentId = 2 }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_GropuTooLongValidation()
        {
            var record = new string('A', ApplicationConstants.ParticipantMaxLen + 1);
            var mockRepository = RepositoryMocks.GetParticipantRepository();
            var handler = new UpdateParticipantHandler(mockRepository, _mapper);
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(new UpdateParticipantRequest
            {
                IncidentId = 2,
                Group = record
            }, CancellationToken.None));
        }
    }
}
