using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Participants.Commands.Delete;
using IoT.IncidentManagement.Application.UnitTests.Mocks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Participants.Delete
{
    public class DeleteParticipantHandlerTests : BaseTest
    {
        [Fact]
        public void Handle_BadRequest()
        {
            var mockRepository = RepositoryMocks.GetParticipantRepository();
            var handler = new DeleteParticipantHandler(mockRepository);

            Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }


        [Fact]
        public void Handle_NotFound()
        {
            var mockRepository = RepositoryMocks.GetParticipantRepository();
            var handler = new DeleteParticipantHandler(mockRepository);

            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(new DeleteParticipantRequest { IncidentId = 55 }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_DeleteParticipantFromRepository()
        {
            var mockRepository = RepositoryMocks.GetParticipantRepository();
            var handler = new DeleteParticipantHandler(mockRepository);
            await handler.Handle(new DeleteParticipantRequest { IncidentId = 2 }, CancellationToken.None);

            var result = (await mockRepository.GetAllAsync()).ToList();

            Assert.Empty(result);
        }
    }
}
