using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Statuses.Commands.Delete;
using IoT.IncidentManagement.Application.UnitTests.Mocks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Statuses.Delete
{
    public class DeleteStatusHandlerTests : BaseTest
    {
        [Fact]
        public void Handle_BadRequest()
        {
            var mockRepository = RepositoryMocks.GetStatusRepository();
            var handler = new DeleteStatusHandler(mockRepository);

            Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }


        [Fact]
        public void Handle_NotFound()
        {
            var mockRepository = RepositoryMocks.GetStatusRepository();
            var handler = new DeleteStatusHandler(mockRepository);

            Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(new DeleteStatusRequest { Id = 55 }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_DeleteStatusFromRepository()
        {
            var mockRepository = RepositoryMocks.GetStatusRepository();
            var handler = new DeleteStatusHandler(mockRepository);
            await handler.Handle(new DeleteStatusRequest { Id = 1 }, CancellationToken.None);

            var result = (await mockRepository.GetAllAsync()).ToList();

            Assert.Equal(2, result.Count);
        }
    }
}
