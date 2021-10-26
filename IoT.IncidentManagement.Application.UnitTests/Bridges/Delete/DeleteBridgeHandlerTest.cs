using IoT.IncidentManagement.Application.Exceptions;
using IoT.IncidentManagement.Application.Features.Bridges.Commands.DeleteBridge;
using IoT.IncidentManagement.Application.Features.Bridges.Commands.Get.List;
using IoT.IncidentManagement.Application.UnitTests.Mocks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace IoT.IncidentManagement.Application.UnitTests.Bridges.Delete
{
    public class DeleteBridgeHandlerTest : BaseTest
    {
        [Fact]
        public async Task Handle_NullRequest()
        {
            var _mockBridgeRepository = RepositoryMocks.GetBridgeRepository();
            var handler = new DeleteBridgeHandler(_mockBridgeRepository);

            await Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(null, CancellationToken.None));
        }


        [Fact]
        public async Task Handle_NotFound()
        {
            var _mockBridgeRepository = RepositoryMocks.GetBridgeRepository();
            var handler = new DeleteBridgeHandler(_mockBridgeRepository);

            await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(new DeleteBridgeRequest { Id = 55 }, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_DeleteBridgeFromRepository()
        {
            var _mockBridgeRepository = RepositoryMocks.GetBridgeRepository();
            var handler = new DeleteBridgeHandler(_mockBridgeRepository);
            await handler.Handle(new DeleteBridgeRequest { Id = 1 }, CancellationToken.None);

            var getHandler = new GetBridgesListHandler(_mockBridgeRepository, _mapper);
            var result = await getHandler.Handle(new GetBridgesListRequest(), CancellationToken.None);

            Assert.Equal(2, result.Count);
        }
    }
}
